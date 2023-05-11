using System;
using System.Collections.Generic;
using Level;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class TerminalData
{
    public string Name;
    public bool IsFinished;
}

public class Terminal : MonoBehaviour, ITouchable, IStorable<TerminalData>
{
    [SerializeField] List<Transform> list = new List<Transform>();
    [SerializeField]private int _levelScene;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private string _id;
    public bool isFinished;
    [SerializeField] private PlayerModules _playerModules;
    private PlayerBehaviour _player;

    public Vector3 ObjectPosition => transform.position;
    public string LevelScene => "SceneLevel " + Convert.ToString(_levelScene);
    private string DataName => "Terminal" + _levelScene;
    public string Id => _id;


    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        _id = Guid.NewGuid().ToString();
    }

    public void Activate(PlayerBehaviour playerBehaviour)
    {
        _player = playerBehaviour;
        _player.GetInventory.Set(_playerModules);
        EnableCanvas(false);
        StateMachine.Instance.ChangeState(new LoadingSceneState(new TerminalState(this), this.LevelScene));
    }
    public void Deactivate()
    {
        
    }

    public void EnableOutline(bool isEnabled)
    {
        EnableCanvas(isEnabled);
        ColorChange(GetComponentsInChildren<Outlines>(), isEnabled);
    }
    
    public void EnterTerminalMode()
    {
        LevelManager.Instance.currentTerminal = this;
    }

    public void ExitTerminalMode()
    {
        SceneManager.UnloadSceneAsync(LevelScene);
        EnableCanvas(true);
        if (isFinished)
        {
            ActivateObjects();
        }
        else
        {
            _player.GetInventory.Unset(_playerModules);
        }
    }

    public void ActivateObjects()
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].GetComponent<IObjectActivated>() != null)
            {
                list[i].GetComponent<IObjectActivated>().ActivateObject();
            }
        }
    }
    
    private void ColorChange(Outlines[] outlines, bool isOn)
    {
        foreach (Outlines outline in outlines)
        {
            outline.enabled = isOn;
        }
    }

    private void EnableCanvas(bool isOn)
    {
        _canvas.SetActive(isOn);
    }
    
    public void LoadData(TerminalData data)
    {
        if (data == null) {
            // Base data
        }
        else
        {
            isFinished = data.IsFinished;
            if(isFinished) ActivateObjects();
        }
    }

    public TerminalData SaveData()
    {
        return new TerminalData()
        {
            Name = DataName,
            IsFinished = isFinished
        };
    }
}
