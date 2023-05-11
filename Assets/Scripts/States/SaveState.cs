using System;
using DG.Tweening;
using UnityEngine;

public class SaveState : IState
{
    public SaveAndLoad SaveAndLoad => Panels.Instance.saveAndLoad;
    private RectTransform _saveAndLoadRect;
    private SaveAndLoad.PanelData _panelData;

    public void Enter()
    {
        SaveAndLoad.onBack = OnBack;
        SaveAndLoad.Show(_panelData = new SaveAndLoad.PanelData{Callback = OnSave, FileList = StoreDataManager.Instance.GetSaveList(), Header = "Save", IsSave = true});
        _saveAndLoadRect = SaveAndLoad.gameObject.GetComponent<RectTransform>();
        _saveAndLoadRect.DOAnchorPos(new Vector2(-250, 0), 0.5f);
    }

    private void OnAutosave()
    {
        return;
    }

    private void OnSave(string fileName)
    {
        StoreDataManager.Instance.DeleteSave(fileName);
        StoreDataManager.Instance.SaveGame("Save " + DateTime.Now.ToString("MM.dd HH-mm-ss"));
        _panelData.FileList = StoreDataManager.Instance.GetSaveList();
        SaveAndLoad.UpdatePanelView(_panelData);
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
