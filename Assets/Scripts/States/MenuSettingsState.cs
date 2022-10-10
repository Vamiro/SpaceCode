using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

internal class MenuSettingsState : IState
{
    public MenuSettnigs MenuSettings => Panels.Instance.menuSettnigs;

    public void Enter()
    {
        MenuSettings.onBack = OnBack;
        MenuSettings.onChange = OnChange;
        MenuSettings.Show(SettingsData.Instance);
    }

    private void OnChange(SettingsData settingsData)
    {
        settingsData.UpdateAudioVolume();
    }

    private void OnBack()
    {
        StateMachine.Instance.ChangeState(new MainMenuState());
    }

    public void Exit()
    {
        SettingsData.Instance.Save();
        MenuSettings.Close();
    }
}

