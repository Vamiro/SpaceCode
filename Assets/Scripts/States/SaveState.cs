using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SaveState : IState
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
        StoreDataManager.Instance.SaveGame("Save1");
    }

    private void OnSave2()
    {
        StoreDataManager.Instance.SaveGame("Save2");
    }

    private void OnSave3()
    {
        StoreDataManager.Instance.SaveGame("Save3");
    }

    private void OnSave4()
    {
        StoreDataManager.Instance.SaveGame("Save4");
    }

    private void OnBack()
    {
        StateMachine.Instance.ChangeState(new MainMenuState());
    }

    public void Exit()
    {
        _saveAndLoadRect.DOAnchorPos(new Vector2(-2500, 0), 0.5f).OnComplete(() => SaveAndLoad.Close());
    }
}
