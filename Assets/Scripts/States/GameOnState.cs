using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOnState : IState
{
    public void Enter()
    {
        MainMenuState.Menu.Close();
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    public void Exit()
    {

    }
}
