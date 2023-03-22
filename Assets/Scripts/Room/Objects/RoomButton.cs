using System.Collections.Generic;
using UnityEngine;

public class RoomButton : MonoBehaviour, IObjectActivated, ITouchable
{
    [SerializeField] private List<Transform> _targets = new List<Transform>();
    private bool _isActive = false;

    public void Activate()
    {
        _isActive = true;
        this.GetComponent<Outlines>().OutlineColor = new Color(0.4f, 1f, 0.4f);
    }

    public void Deactivate()
    {
        _isActive = false;
        this.GetComponent<Outlines>().OutlineColor = Color.HSVToRGB(1f, 0.3f, 0.25f);
    }

    public void PressButton()
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

    public void ShowOutline(PlayerBehaviour player)
    {
        ColorChange(GetComponentsInChildren<Outlines>(), true);
    }

    public void HideOutline(PlayerBehaviour player)
    {
        ColorChange(GetComponentsInChildren<Outlines>(), false);
    }

    private void ColorChange(Outlines[] outlines, bool isOn)
    {
        foreach (Outlines outline in outlines)
        {
            outline.enabled = isOn;
        }
    }
}
