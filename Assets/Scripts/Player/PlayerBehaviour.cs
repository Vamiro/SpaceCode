using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PlayerData {
    public Vector3 Position;
    public Quaternion Rotation;
}

public class PlayerBehaviour : MonoBehaviour, IStorable<PlayerData>
{
    [SerializeField] private GameObject _playerRoot;

    private HashSet<Terminal> _terminals = new HashSet<Terminal>();
    private HashSet<RoomButton> _buttons = new HashSet<RoomButton>();

    public Terminal currentTerminal;

    public Terminal ActivateNearestTerminalOrButton()
    {
        var terminal = FindNearestObject(_terminals);
        if (terminal != null)
        {
            currentTerminal = terminal;
            return currentTerminal;
        }
        var button = FindNearestObject(_buttons);
        if (button != null)
        {
            button.PressButton();
        }
        return null;
    }

    public void ExitGameMode()
    {
        _playerRoot.SetActive(false);
    }
    
    public void EnterGameMode()
    {
        _playerRoot.SetActive(true);
    }
    
    private T FindNearestObject<T>(IEnumerable<T> objects) where T : Component, ITouchable
    {
        return objects.OrderBy((T obj) => Vector3.Distance(obj.transform.position, transform.position)).FirstOrDefault();
    }

    private void HandleTouchableObject(ITouchable obj, bool isEnter)
    {
        if (obj != null)
        {
            if (obj is Terminal terminal)
            {
                if (isEnter)
                {
                    _terminals.Add(terminal);
                }
                else
                {
                    _terminals.Remove(terminal);
                }
            }
            else if (obj is RoomButton button)
            {
                if (isEnter)
                {
                    _buttons.Add(button);
                }
                else
                {
                    _buttons.Remove(button);
                }
            }
            obj.EnableOutline(isEnter);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        var terminal = collider.GetComponentInParent<ITouchable>();
        HandleTouchableObject(terminal, true);
        var button = collider.GetComponent<RoomButton>();
        HandleTouchableObject(button, true);
    }

    private void OnTriggerExit(Collider collider)
    {
        var terminal = collider.GetComponentInParent<ITouchable>();
        HandleTouchableObject(terminal, false);
        var button = collider.GetComponent<RoomButton>();
        HandleTouchableObject(button, false);
    }
    
    [SerializeField] private string _id = Guid.NewGuid().ToString();
    public string Id => _id;
    
    public void LoadData(PlayerData data)
    {
        if (data == null) {
            // Base data
        }
        else {
            transform.position = data.Position;
            transform.rotation = data.Rotation;
        }
    }

    public PlayerData SaveData()
    {
        return new PlayerData()
        {
            Position = transform.position,
            Rotation = transform.rotation
        };
    }
}
