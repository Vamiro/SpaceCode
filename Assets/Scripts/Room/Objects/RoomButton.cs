using System.Collections.Generic;
using UnityEngine;

public class RoomButton : MonoBehaviour, IObjectActivated, ITouchable
{
    [SerializeField] private List<Transform> _targets = new List<Transform>();
    [SerializeField] private bool _isActive = false;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _notActiveCanvas;

    public Vector3 ObjectPosition => transform.position;

    public void ActivateObject()
    {
        _isActive = true;
    }

    public void DeactivateObject()
    {
        _isActive = false;
    }

    public void Activate(PlayerBehaviour playerBehaviour)
    {
        if (_isActive && _targets != null)
        {
            for (int i = 0; i < _targets.Count; i++)
            {
                if (_targets[i].GetComponent<IButton>() != null)
                {
                    _targets[i].GetComponent<IButton>().ButtonPressed();
                }
            }
        }
        else
        {
            Debug.Log("Button inactive");
        }
    }

    public void Deactivate()
    {
        
    }

    public void EnableOutline(bool isEnabled)
    {
        EnableCanvas(isEnabled);
        ColorChange(GetComponentsInChildren<Outlines>(), isEnabled);
    }

    private void EnableCanvas(bool isOn)
    {
        if(_isActive) _canvas.SetActive(isOn);
        else _notActiveCanvas.SetActive(isOn);
    }

    private void ColorChange(Outlines[] outlines, bool isOn)
    {
        
        foreach (Outlines outline in outlines)
        {
            if (_isActive) outline.OutlineColor = new Color(0.4f, 1f, 0.4f);
            outline.enabled = isOn;
        }
    }
}
