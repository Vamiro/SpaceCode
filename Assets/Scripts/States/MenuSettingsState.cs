using DG.Tweening;
using UnityEngine;

internal class MenuSettingsState : IState
{
    public MenuSettnigs MenuSettings => Panels.Instance.menuSettnigs;
    private RectTransform _menuSettingsRect;
    public void Enter()
    {
        MenuSettings.onBack = OnBack;
        MenuSettings.onChange = OnChange;
        MenuSettings.Show(SettingsData.Instance);

        _menuSettingsRect = MenuSettings.gameObject.GetComponent<RectTransform>();
        _menuSettingsRect.DOAnchorPos(new Vector2(0, 30), 0.5f);
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
        _menuSettingsRect.DOAnchorPos(new Vector2(0, 1100), 0.5f).OnComplete(() => MenuSettings.Close());

    }

    public void HandleInput()
    {
        if (Input.GetButtonUp("Esc")){ StateMachine.Instance.ChangeState(new MainMenuState()); }
    }
}