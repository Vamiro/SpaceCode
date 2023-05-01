using UnityEngine;

internal class InitState : IState
{
    public LoadingScreen LoadingScreen => Panels.Instance.loadingScreen;

    public void Enter()
    {
        InitializeAudio();
        LoadingScreen.Show();
        //await Task.Delay(TimeSpan.FromSeconds(5));
        StateMachine.Instance.ChangeState(new MainMenuState());
    }

    private void InitializeAudio()
    {
        Debug.Log("InitializeAudio done!");
        SettingsData.Instance.UpdateAudioVolume();
    }

    public void Exit()
    {
        LoadingScreen.Close();
    }

    public void HandleInput()
    {
    }
}