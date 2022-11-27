using UnityEngine;
using UnityEngine.SceneManagement;

internal class MainMenuState : IState
{
    public static Menu Menu => Panels.Instance.menu;

    public void Enter()
    {
        Menu.onStart = OnStart;
        Menu.onSettings = OnSettings;
        Menu.onCredits = OnCredits;
        Menu.onExit = OnExit;
        Menu.Show();
    }

    private void OnStart()
    {
        StateMachine.Instance.ChangeState(new GameOnState());
        Debug.Log("OnStart");
    }

    private void OnSettings()
    {
        StateMachine.Instance.ChangeState(new MenuSettingsState());
        Debug.Log("OnSettings");
    }

    private void OnCredits()
    {
        StateMachine.Instance.ChangeState(new MenuCreditsState());
        Debug.Log("OnCredits");
    }

    private void OnExit()
    {
        Debug.Log("OnExit");
        Application.Quit();
    }

    public void Exit()
    {
    }
}