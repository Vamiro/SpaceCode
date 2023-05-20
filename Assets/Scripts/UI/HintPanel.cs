using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HintPanel : UIPanel<string>
{
    [SerializeField] private Button _backButton;
    [SerializeField] private TMP_Text _hintText;

    public UnityAction onBack;

    private void Awake()
    {
        _backButton.onClick.AddListener(() => onBack?.Invoke());
        onBack = OnBack;
    }

    private void OnBack()
    {
        this.Close();
    }

    protected override void OnShow()
    {
        _hintText.text = _argument;
    }

    protected override void OnClose()
    {
        _hintText.text = "";
    }
}
