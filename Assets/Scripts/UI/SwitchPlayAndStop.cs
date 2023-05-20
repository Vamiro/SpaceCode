using System;
using MG_BlocksEngine2.Core;
using MG_BlocksEngine2.Environment;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPlayAndStop : BehaviourSingleton<SwitchPlayAndStop>
{
    private Button Button;
    [SerializeField] private Image _image;
    [SerializeField] private Image _childrenImage;
    [SerializeField] private Sprite _playSprite;
    [SerializeField] private Sprite _stopSprite;

    [SerializeField] private TMP_Text _blocksText;
    [SerializeField] private int _maxBlocks;

    private int _blocksCount;

    public int SetBlocksCount
    {
        get => _blocksCount;
        set
        {
            _blocksCount = value;
            CheckProgrammEnv();
        }
    }

    private void Awake()
    {
        base.Awake();
        Button = gameObject.GetComponent<Button>();
        if (Button != null)
        {
            Button.onClick.AddListener(() => BE2_ExecutionManager.Instance.Play());
            _image.color = new Color(0.495283f, 1f, 0.5625787f);
            _childrenImage.sprite = _playSprite;
        }
        if(_blocksText != null)
        {
            CheckProgrammEnv();
        }
    }
    void OnEnable()
    {
        BE2_MainEventsManager.Instance.StartListening(BE2EventTypes.OnPlay, SwitchToStop);
        BE2_MainEventsManager.Instance.StartListening(BE2EventTypes.OnStop, SwitchToPlay);
    }

    void OnDisable()
    {
        BE2_MainEventsManager.Instance.StopListening(BE2EventTypes.OnPlay, SwitchToStop);
        BE2_MainEventsManager.Instance.StopListening(BE2EventTypes.OnStop, SwitchToPlay);
    }

    public void SwitchToPlay()
    {
        _image.color = new Color(0.495283f, 1f, 0.5625787f);
        _childrenImage.sprite = _playSprite;
        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(() => BE2_ExecutionManager.Instance.Play());
    }
    public void SwitchToStop()
    {
        _image.color = new Color(1f, 0.4764151f, 0.4764151f);
        _childrenImage.sprite = _stopSprite;
        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(() => BE2_ExecutionManager.Instance.Stop());
    }

    public void CheckProgrammEnv()
    {
        _blocksText.text = _blocksCount + "/" + _maxBlocks;
        if (_maxBlocks <= 0)
        {
            _blocksText.color = new Color(0.495283f, 1f, 0.5625787f);
            _blocksText.text = _blocksCount + "/∞";
        }
        else if (_blocksCount <= _maxBlocks && _blocksCount > 0)
        {
            Button.enabled = true;
            _blocksText.color = new Color(0.495283f, 1f, 0.5625787f);
        }
        else
        {
            Button.enabled = false;
            _blocksText.color = new Color(1f, 0.4764151f, 0.4764151f);
        }
    }
}
