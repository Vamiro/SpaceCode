using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SaveAndLoad : UIPanel
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _autosaveButton;
    [SerializeField] private Button _save1Button;
    [SerializeField] private Button _save2Button;
    [SerializeField] private Button _save3Button;
    [SerializeField] private Button _save4Button;

    public UnityAction onBack;
    public UnityAction onAutosave;
    public UnityAction onSave1;
    public UnityAction onSave2;
    public UnityAction onSave3;
    public UnityAction onSave4;

    private void Awake()
    {
        _backButton.onClick.AddListener(() => onBack?.Invoke());
        _autosaveButton.onClick.AddListener(() => onAutosave?.Invoke());
        _save1Button.onClick.AddListener(() => onSave1?.Invoke());
        _save2Button.onClick.AddListener(() => onSave2?.Invoke());
        _save3Button.onClick.AddListener(() => onSave3?.Invoke());
        _save4Button.onClick.AddListener(() => onSave4?.Invoke());
    }

    protected override void OnShow()
    {
    }

    protected override void OnClose()
    {
    }
}
