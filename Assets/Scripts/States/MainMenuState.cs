using UnityEngine;

internal class MainMenuState : IState
{
    public static Menu Menu => Panels.Instance.menu;


    public void Enter()
    {
        Menu.onContinue = OnContinue;
        Menu.onNewGame = OnNewGame;
        Menu.onBack = OnBack;
        Menu.onSave = OnSave;
        Menu.onLoad = OnLoad;
        Menu.onSettings = OnSettings;
        Menu.onCredits = OnCredits;
        Menu.onExit = OnExit;
        Menu.Show();
    }

    private void OnBack()
    {
        StateMachine.Instance.ChangeState(new GameOnState());
    }

    private void OnLoad()
    {
        StateMachine.Instance.ChangeState(new LoadState());
    }

    private void OnSave()
    {
        StateMachine.Instance.ChangeState(new SaveState());
    }

    private void OnContinue()
    {
        
    }

    private void OnNewGame()
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

    public void HandleInput()
    {
        if (Input.GetButtonUp("Esc") && StateMachine.Instance.IsGameOn){ StateMachine.Instance.ChangeState(new GameOnState()); }
    }
}