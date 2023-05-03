using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadState : IState
{
    public SaveAndLoad SaveAndLoad => Panels.Instance.saveAndLoad;
    private RectTransform _saveAndLoadRect;

    public void Enter()
    {
        SaveAndLoad.onBack = OnBack;
        SaveAndLoad.onAutosave = OnAutosave;
        SaveAndLoad.onSave1 = OnSave1;
        SaveAndLoad.onSave2 = OnSave2;
        SaveAndLoad.onSave3 = OnSave3;
        SaveAndLoad.onSave4 = OnSave4;
        SaveAndLoad.Show();

        _saveAndLoadRect = SaveAndLoad.gameObject.GetComponent<RectTransform>();
        _saveAndLoadRect.DOAnchorPos(new Vector2(250, 0), 0.5f);
    }

    private void OnAutosave()
    {
        return;
    }

    private void OnSave1()
    {
        LoadSave("Save1");
    }

    private void OnSave2()
    {
        LoadSave("Save2");
    }

    private void OnSave3()
    {
        LoadSave("Save3");
    }

    private void OnSave4()
    {
        LoadSave("Save4");
    }

    private void LoadSave(string save)
    {
        _saveAndLoadRect.DOAnchorPos(new Vector2(-2500, 0), 0.5f)
            .OnComplete(() =>
            {
                StateMachine.Instance.ChangeState(StateMachine.Instance.IsGameOn
                    ? new LoadingSceneState(new GameOnState(save))
                    : new LoadingSceneState(new GameOnState(save), "TheFirstRoom"));
            });
    }

    private void OnBack()
    {
        if(DOTween.PlayingTweens() == null) _saveAndLoadRect.DOAnchorPos(new Vector2(-2500, 0), 0.5f).OnComplete(() => StateMachine.Instance.ChangeState(new MainMenuState()));
    }

    public void Exit()
    {
        SaveAndLoad.Close();
    }

    public void HandleInput()
    {
        if (Input.GetButtonUp("Esc")) OnBack();
    }
}