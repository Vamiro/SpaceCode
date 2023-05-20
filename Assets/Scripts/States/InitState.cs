using UnityEngine;

internal class InitState : IState
{
    public void Enter()
    {
        InitializeAudio();
        StateMachine.Instance.ChangeState(new MainMenuState());
    }

    private void InitializeAudio()
    {
        Debug.Log("InitializeAudio done!");
        SettingsData.Instance.UpdateAudioVolume();
    }

    public void Exit()
    {
    }

    public void HandleInput()
    {
    }
}