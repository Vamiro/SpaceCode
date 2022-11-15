using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuCredits : UIPanel
{
    [SerializeField] private Button _backButton;

    public UnityAction onBack;

    private void Awake()
    {
        _backButton.onClick.AddListener(() => onBack?.Invoke());
    }

    protected override void OnShow()
    {
    }

    protected override void OnClose()
    {
    }
}
