using MG_BlocksEngine2.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPlayAndStop : MonoBehaviour
{
    private Button Button;
    [SerializeField] private Image _image;
    [SerializeField] private Image _childrenImage;
    [SerializeField] private Sprite _playSprite;
    [SerializeField] private Sprite _stopSprite;

    private void Awake()
    {
        Button = gameObject.GetComponent<Button>();
        if (Button != null)
        {
            Button.onClick.AddListener(() => BE2_ExecutionManager.Instance.Play());
            _image.color = new Color(0.495283f, 1f, 0.5625787f);
            _childrenImage.sprite = _playSprite;
        }
    }
    void OnEnable()
    {
        BE2_MainEventsManager.Instance.StartListening(BE2EventTypes.OnPlay, SwitchToStop);
        BE2_MainEventsManager.Instance.StartListening(BE2EventTypes.OnStop, SwitchToPlay);
    }

    void OnDisable()
    {
        BE2_MainEventsManager.Instance.StopListening(BE2EventTypes.OnPrimaryKeyUpEnd, SwitchToStop);
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
}
