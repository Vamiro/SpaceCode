using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOnState : IState
{
    public void Enter()
    {
        var sceneOperation = SceneManager.LoadSceneAsync("Scenes/TheFirstRoom", LoadSceneMode.Additive);
        sceneOperation.completed += (e) => {
            StartUp.Instance.camera.SetActive(false);
            MainMenuState.Menu.Close();
        };
    }

    public void Exit()
    {

    }
}
