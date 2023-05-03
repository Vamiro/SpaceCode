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
        StateMachine.Instance.ChangeState(new LoadingSceneState(new GameOnState(), "TheFirstRoom"));
    }

    private void OnSettings()
    {
        StateMachine.Instance.ChangeState(new MenuSettingsState());
    }

    private void OnCredits()
    {
        StateMachine.Instance.ChangeState(new MenuCreditsState());
    }

    private void OnExit()
    {
        Application.Quit();
    }

    public void Exit()
    {
        Menu.onContinue = null;
        Menu.onNewGame = null;
        Menu.onBack = null;
        Menu.onSave = null;
        Menu.onLoad = null;
        Menu.onSettings = null;
        Menu.onCredits = null;
        Menu.onExit = null;
    }

    public void HandleInput()
    {
        if (Input.GetButtonUp("Esc") && StateMachine.Instance.IsGameOn){ StateMachine.Instance.ChangeState(new GameOnState()); }
    }
}