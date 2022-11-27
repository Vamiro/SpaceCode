using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

public class Terminal : MonoBehaviour, ITouchable
{
    [SerializeField] PlayerBehaviour Player;
    [SerializeField] Transform Root;
    [SerializeField] Transform cameraPosition;
    private void Update()
    {
        if(Player != null && Input.GetButtonUp("Activate"))
        {
            Debug.Log("Terminal Activated");
            Player.ToTerminalMode(cameraPosition);
        }
    }

    public void Activate(PlayerBehaviour player)
    {
        ColorChange(GetComponentsInChildren<Outlines>(), true);
        Player = player;
    }

    public void Deactivate(PlayerBehaviour player)
    {
        ColorChange(GetComponentsInChildren<Outlines>(), false);
        Player = null;
    }

    private void ColorChange(Outlines[] outlines, bool isOn)
    {
        foreach (Outlines outline in outlines)
        {
            outline.enabled = isOn;
        }
    }
}
