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
    public bool isFinished;
    
    private string DataName => "Terminal" + _levelScene;
    [SerializeField] private string _id;
    public string Id => _id;

    public static Terminal CurrentTerminal { get; private set; }

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        _id = Guid.NewGuid().ToString();
    }
    
    public void EnableOutline(bool isEnabled)
    {
        ColorChange(GetComponentsInChildren<Outlines>(), isEnabled);
    }
    
    public void ToTerminalMode(Action complete)
    {

        ThisActivated();
        var sceneOperation = SceneManager.LoadSceneAsync("Scenes/SceneLevel" + Convert.ToString(_levelScene), LoadSceneMode.Additive);
        sceneOperation.completed += (e) => complete();
    }

    public void ToWorldMode(Action complete)
    {
        var sceneOperation = SceneManager.UnloadSceneAsync("Scenes/SceneLevel" + Convert.ToString(_levelScene));
        sceneOperation.completed += (e) => complete();
        if (isFinished)
        {
            ActivateObjects();
        }
        ThisDeactivated();
    }

    public void ThisActivated()
    {
        CurrentTerminal = this;
    }
    public void ThisDeactivated()
    {
        Debug.Assert(CurrentTerminal == this);
        CurrentTerminal = null;
    }

    public void ActivateObjects()
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].GetComponent<IObjectActivated>() != null)
            {
                list[i].GetComponent<IObjectActivated>().Activate();
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

    
    public void LoadData(TerminalData data)
    {
        if (data == null) {
            // Base data
        }
        else
        {
            isFinished = data.IsFinished;
            ActivateObjects();
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
