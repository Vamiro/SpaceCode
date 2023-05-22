using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;


public class SaveAndLoad : UIPanel<SaveAndLoad.PanelData>
{
    public class PanelData
    {
        public bool IsSave;
        public string Header;
        public string[] FileList;
        public Action<string> Callback;
    }
    [SerializeField] private Button _backButton;
    [SerializeField] private TMP_Text _header;
    [SerializeField] private SimpleList<SaveButtonView, SaveButtonView.Data> _buttonList;

    public UnityAction onBack;

    private void Awake()
    {
        _backButton.onClick.AddListener(() => onBack?.Invoke());
    }

    protected override void OnShow()
    {
        _header.text = _argument.Header;
        UpdatePanelView(_argument);
    }

    private void OnCallback(SaveButtonView.Data obj)
    {
        _argument.Callback.Invoke(obj.FileName);
    }

    public void UpdatePanelView(PanelData panelData)
    {
        var list = panelData.FileList.Select(s => new SaveButtonView.Data{Callback = OnCallback,ButtonName = s, FileName = s}).ToList();
        if (list.Count() < 10 && StateMachine.Instance.IsGameOn && panelData.IsSave)
        {
            list.Add(new SaveButtonView.Data{Callback = OnCallback, ButtonName = "Новое сохранение"});
        }
        list.Reverse();
        _buttonList.SetData(list);
    }

    protected override void OnClose()
    {
    }
}
