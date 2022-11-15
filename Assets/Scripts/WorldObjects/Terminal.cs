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
        ColorChange(GetComponentsInChildren<MeshRenderer>(), Color.red);
        Player = player;
    }

    public void Deactivate(PlayerBehaviour player)
    {
        ColorChange(GetComponentsInChildren<MeshRenderer>(), Color.white);
        Player = null;
    }

    private void ColorChange(MeshRenderer[] meshRenderer, Color color)
    {
        foreach (MeshRenderer renderer in meshRenderer)
        {
            renderer.material.color = color;
        }
    }
}
