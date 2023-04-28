using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStorable
{
    string Id { get; } // id of an object
    
    void Load(string data); // load default data witch is string var

    string Save(); //default save string 
}

public interface IStorable<T> : IStorable {
    void IStorable.Load(string data) => LoadData(string.IsNullOrEmpty(data) ? default : JsonUtility.FromJson<T>(data));
    
    string IStorable.Save() => JsonUtility.ToJson(SaveData());

    void LoadData(T data);
    
    T SaveData();
}


    /*public interface IStorable {
        string Id { get; }
        void Load(string data);
        string Save();
    }


    public interface IStorable<T> : IStorable {

        void IStorable.Load(string data) => LoadData(string.IsNullOrEmpty(data) ? null : JsonUtility.FromJson<T>(data) ?? null);
        string IStorable.Save() => JsonUtility.Save(SaveData());

        void LoadData(T data);
        T SaveData();
    }


    [Serializable]
    public class PlayerData {
        public Vector3 Position;
        public Quaternion Rotation;
        public string Name;
    }
    
    public class PlayerBehavior : MonoBehavior, IStorable<PlayerData> {

        [SerializeField] private string _id = Guid.NewGuid().ToString(); 

        public string Id => _id;

        public void LoadData(PlayerData data) {
            if (data == null) {
                // Base data
            }
            else {
                Transform.position = data.Position;
                // 
            }
        }

        public PlayerData SaveData() {
            return new PlayerData() {
                Position =  Transform.position,
                .. 
            }
        }

    }


    public struct StoreItem {
        public string Id;
        public string Value;
    }
    public class GameData {

        public List<StoreItem> Items = new List<StoreItem>();

    }
    
    public class StoreManager {

        IEnumerable<IStorable> Objects() => null;

        void Load(string file) {
            GameData gameData = StorageData.Load<GameData>(file);
            var dataMap = gameData.Items.ToDictionary(item => item.Id, item => item.Value);
            foreach (var storable in Objects()) {
                storable.Load(dataMap.TryGetValue(storable.Id, out var value) ? value : null);
            }
        }

        void Save(string file) {
            var data = new GameData() {
                Items = Objects()
                    .Select(storable => new StoreItem() { Id = storable.Id, Value = storable.Save() })
                    .ToList()
            };
            
            StorageData.Save<GameData>(data);
        }
    }*/