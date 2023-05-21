using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HintPanel : UIPanel<int>
{
    [SerializeField] private Button _backButton;
    [SerializeField] private TMP_Text _hintText;
    private HintManeger HintManeger = new HintManeger();

    public UnityAction onBack;

    private void Awake()
    {
        _backButton.onClick.AddListener(() => onBack?.Invoke());
    }

    protected override void OnShow()
    {
        _hintText.text = HintManeger.hintList[_argument];
    }

    protected override void OnClose()
    {
        _hintText.text = "";
    }
}
