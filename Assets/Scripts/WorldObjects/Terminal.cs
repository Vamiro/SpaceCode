using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Terminal : MonoBehaviour, ITouchable
{
    [SerializeField] Transform Root;

    [SerializeField] List<Transform> list = new List<Transform>();

    [SerializeField]private int _levelScene;

    public static Terminal CurrentTerminal { get; private set; }

    public void ShowOutline(PlayerBehaviour player)
    {
        ColorChange(GetComponentsInChildren<Outlines>(), true);
    }

    public void HideOutline(PlayerBehaviour player)
    {
        ColorChange(GetComponentsInChildren<Outlines>(), false);
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
        if (LevelManager.Instance._isFinished)
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
}
