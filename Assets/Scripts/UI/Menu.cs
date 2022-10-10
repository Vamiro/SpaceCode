using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Menu : UIPanel
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _exitButton;

    public UnityAction onStart;
    public UnityAction onSettings;
    public UnityAction onCredits;
    public UnityAction onExit;

    private void Awake()
    {
        _startButton.onClick.AddListener(() => onStart?.Invoke());
        _settingsButton.onClick.AddListener(() => onSettings?.Invoke());
        _creditsButton.onClick.AddListener(() => onCredits?.Invoke());
        _exitButton.onClick.AddListener(() => onExit?.Invoke());
    }

    protected override void OnShow()
    {

    }

    protected override void OnClose()
    {

    }
}
