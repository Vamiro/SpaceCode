using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StoreDataManager : BehaviourSingleton<StoreDataManager>
{
    private GameData _gameData;
    private List<IStorable> _objectsData;

    private void Start()
    {
        _objectsData = FindAllDataPersistenceObjects();
    }

    private List<IStorable> FindAllDataPersistenceObjects()
    {
        IEnumerable<IStorable> objectsData = FindObjectsOfType<MonoBehaviour>().OfType<IStorable>();
        return new List<IStorable>(objectsData);
    }

    public void NewGame()
    {
        //initializing New GameData
        this._gameData = new GameData();
    }

    public void LoadGame(string file)
    {
        _objectsData = FindAllDataPersistenceObjects();
        GameData gameData = Storage.Load<GameData>(file);
        
        if (gameData.Items == null)
        {
            return;
        }
        var dataMap = gameData.Items.ToDictionary(item => item.Id, item => item.Value);

        foreach (var objectData in _objectsData)
        {
            objectData.Load(dataMap.TryGetValue(objectData.Id, out var value) ? value : null);
        }
    }

    public void SaveGame(string file)
    {
        _objectsData = FindAllDataPersistenceObjects();
        var data = new GameData {
            Items = _objectsData.Select(storable => new StoreItem() { Id = storable.Id, Value = storable.Save() }).ToList()
        };
        Storage.Save(data, file);
    }
}
