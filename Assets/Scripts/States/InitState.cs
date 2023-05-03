using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEditor.SearchService.SceneSearch;

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