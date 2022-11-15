using System;
using System.Threading.Tasks;
using UnityEngine;

internal class InitState : IState
{
    public LoadingScreen LoadingScreen => Panels.Instance.loadingScreen;

    public async void Enter()
    {
        InitializeAudio();
        LoadingScreen.Show();
        await Task.Delay(TimeSpan.FromSeconds(5));
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
}