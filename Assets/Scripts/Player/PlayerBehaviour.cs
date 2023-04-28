using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerData {
    public Vector3 Position;
    public Quaternion Rotation;
}

public class PlayerBehaviour : MonoBehaviour, IStorable<PlayerData>
{
    public enum PlayerState { InWorld, InTerminal, InMenu }
    public PlayerState playerState = PlayerState.InWorld;

    [SerializeField] private GameObject _world;
    [SerializeField] private GameObject _playerRoot;

    private HashSet<Terminal> _terminals = new HashSet<Terminal>();
    private HashSet<RoomButton> _buttons = new HashSet<RoomButton>();

    public Terminal currentTerminal;
    
    public delegate void PlayerStateChangedHandler(PlayerState newState);
    public event PlayerStateChangedHandler OnPlayerStateChanged;

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        switch (playerState)
        {
            case PlayerState.InWorld:
                CheckWorldInput();
                break;
            case PlayerState.InTerminal:
                CheckTerminalInput();
                break;
            case PlayerState.InMenu:
                CheckMenuInput();
                break;
        }
    }

    private void CheckWorldInput()
    {
        if (Input.GetButtonUp("Activate"))
        {
            var terminal = FindNearestObject(_terminals);
            if (terminal != null)
            {
                currentTerminal = terminal;
                terminal.ToTerminalMode(() =>
                {
                    ExitGameMode(PlayerState.InTerminal, true);
                });
            }
            var button = FindNearestObject(_buttons);
            if (button != null)
            {
                button.PressButton();
            }
        }
        else if (Input.GetButtonUp("Esc"))
        {
            ExitGameMode(PlayerState.InMenu, true);
        }
    }

    private void CheckTerminalInput()
    {
        if (Input.GetButtonUp("Esc"))
        {
            var terminal = FindNearestObject(_terminals);
            if (terminal != null)
            {
                terminal.ToWorldMode(() => EnterGameMode(true));
            }
        }
    }

    private void CheckMenuInput()
    {
        if (Input.GetButtonUp("Esc"))
        {
            EnterGameMode(true);
        }
    }

    public void ExitGameMode(PlayerState pS, bool isInvoker)
    {
        _playerRoot.SetActive(false);
        playerState = pS;
        if(isInvoker) OnPlayerStateChanged?.Invoke(playerState);
    }
    
    public void EnterGameMode(bool isInvoker)
    {
        _playerRoot.SetActive(true);
        playerState = PlayerState.InWorld;
        if(isInvoker) OnPlayerStateChanged?.Invoke(playerState);
        Debug.Log(transform.position);
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
