using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Menu : UIPanel
{
    [SerializeField] private Button _backToGameButton;
    [SerializeField] private Button _continueGameButton;
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _saveGameButton;
    [SerializeField] private Button _loadGameButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _exitButton;

    public UnityAction onBack;
    public UnityAction onContinue;
    public UnityAction onNewGame;
    public UnityAction onSave;
    public UnityAction onLoad;
    public UnityAction onSettings;
    public UnityAction onCredits;
    public UnityAction onExit;

    private void Awake()
    {
        _backToGameButton.onClick.AddListener(() => onBack?.Invoke());
        _continueGameButton.onClick.AddListener(() => onContinue?.Invoke());
        _newGameButton.onClick.AddListener(() => onNewGame?.Invoke());
        _saveGameButton.onClick.AddListener(() => onSave?.Invoke());
        _loadGameButton.onClick.AddListener(() => onLoad?.Invoke());
        _settingsButton.onClick.AddListener(() => onSettings?.Invoke());
        _creditsButton.onClick.AddListener(() => onCredits?.Invoke());
        _exitButton.onClick.AddListener(() => onExit?.Invoke());
    }

    protected override void OnShow()
    {
        if (StateMachine.Instance.IsGameOn)
        {
            _newGameButton.gameObject.SetActive(false);
            _backToGameButton.gameObject.SetActive(true);
            _saveGameButton.gameObject.SetActive(true);
        }
    }

    protected override void OnClose()   
    {

    }
}
