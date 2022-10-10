using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuSettnigs : UIPanel<SettingsData>
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Slider _generalSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _SFXSlider;

    public UnityAction onBack;
    public UnityAction<SettingsData> onChange;

    private void Awake()
    {
        _backButton.onClick.AddListener(() => onBack?.Invoke());
        _generalSlider.onValueChanged.AddListener(OnChangeSlider);
        _musicSlider.onValueChanged.AddListener(OnChangeSlider);
        _SFXSlider.onValueChanged.AddListener(OnChangeSlider);
    }

    protected override void SetUp(SettingsData settingsData)
    {
        _generalSlider.value = settingsData.generalVolume;
        _musicSlider.value = settingsData.musicVolume;
        _SFXSlider.value = settingsData.SFXlVolume;
    }

    private void OnChangeSlider(float _)
    {
        _argument.generalVolume = _generalSlider.value;
        _argument.musicVolume = _musicSlider.value;
        _argument.SFXlVolume = _SFXSlider.value;
        onChange?.Invoke(_argument);
    }

    protected override void OnShow()
    {
    }
    protected override void OnClose()
    {
    }
}

