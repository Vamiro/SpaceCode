using System;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;


public class SaveButtonView: MonoBehaviour, IDataView<SaveButtonView.Data>
{
    public class Data
    {
        public string ButtonName { get; set; }
        public string FileName { get; set; }
        public Action<Data> Callback { get; set; }
    }

    [SerializeField]private Button _button;
    [SerializeField]private TMP_Text _buttonText;
    private Data _data;

    private void Awake()
    {
        _button.onClick.AddListener(() => _data.Callback?.Invoke(_data));
    }

    public void SetData(Data data)
    {
        _data = data;
        _buttonText.text = data.ButtonName;
    }
}
