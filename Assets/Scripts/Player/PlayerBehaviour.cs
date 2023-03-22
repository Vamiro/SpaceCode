using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private enum PlayerState { InWorld, InTerminal }
    private PlayerState playerState = PlayerState.InWorld;

    [SerializeField] private GameObject _world;
    [SerializeField] private GameObject _playerRoot;

    private HashSet<Terminal> _terminals = new HashSet<Terminal>();
    private HashSet<RoomButton> _buttons = new HashSet<RoomButton>();

    private void Update()
    {
        if (playerState == PlayerState.InWorld && Input.GetButtonUp("Activate"))
        {

            var T = FindTerminal();
            if (T != null)
            {
                playerState = PlayerState.InTerminal;
                T.ToTerminalMode(() => _playerRoot.SetActive(false));
            }
            var B = FindButton();
            if (B != null)
            {
                B.PressButton();
            }

        }
        if (playerState == PlayerState.InTerminal && Input.GetButtonUp("Esc"))
        {
            var T = FindTerminal();
            if (T != null)
            {
                playerState = PlayerState.InWorld;
                T.ToWorldMode(() => _playerRoot.SetActive(true));
            }
        }
    }

    private Terminal FindTerminal()
    {
        return _terminals.OrderBy((Terminal T) => Vector3.Distance(T.transform.position, transform.position)).FirstOrDefault();
    }
    private RoomButton FindButton()
    {
        return _buttons.OrderBy((RoomButton T) => Vector3.Distance(T.transform.position, transform.position)).FirstOrDefault();
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Trigger Enter");
        var terminal = collider.GetComponentInParent<ITouchable>();
        if (terminal != null)
        {
            if (terminal is Terminal T)
            {
                _terminals.Add(T);
            }
            terminal.ShowOutline(this);
        }
        var button = collider.GetComponent<RoomButton>();
        if (button != null)
        {
            if (button is RoomButton B)
            {
                _buttons.Add(B);
            }
            button.ShowOutline(this);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        Debug.Log("Trigger Exit");
        var terminal = collider.GetComponentInParent<ITouchable>();
        if (terminal != null)
        {
            if (terminal is Terminal T)
            {
                _terminals.Remove(T);
            }
            terminal.HideOutline(this);
        }
        var button = collider.GetComponent<RoomButton>();
        if (button != null)
        {
            if (button is RoomButton B)
            {
                _buttons.Remove(B);
            }
            button.HideOutline(this);
        }
    }
}
