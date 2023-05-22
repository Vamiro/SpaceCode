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
        SaveAndLoad.Show(new SaveAndLoad.PanelData{Callback = LoadSave, FileList = StoreDataManager.Instance.GetSaveList(), Header = "Загрузка", IsSave = false});

        _saveAndLoadRect = SaveAndLoad.gameObject.GetComponent<RectTransform>();
        _saveAndLoadRect.DOAnchorPos(new Vector2(-250, 0), 0.5f);
    }

    private void LoadSave(string save)
    {
        _saveAndLoadRect.DOAnchorPos(new Vector2(-2500, 0), 0.5f)
            .OnComplete(() =>
            {
                if (StateMachine.Instance.IsGameOn) SceneManager.UnloadSceneAsync("TheFirstRoom");
                StateMachine.Instance.IsGameOn = true;
                StateMachine.Instance.ChangeState(new LoadingSceneState(new GameOnState(save), "TheFirstRoom"));
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