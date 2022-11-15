using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    CameraController cameraController;
    PlayerController playerController;
    private Vector3 _prevPosition;
    private Quaternion _prevRotation;

    private enum PlayerState { InWorld, InTerminal }
    private PlayerState playerState = PlayerState.InWorld;

    private void Update()
    {
        if(playerState == PlayerState.InTerminal && Input.GetButtonUp("Esc"))
        {
            ToWorldMode();
        }
    }

    private void Awake()
    {
        cameraController = GetComponentInChildren<CameraController>();
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        var touchable = collider.GetComponentInParent<ITouchable>();
        if (touchable != null) touchable.Activate(this);
    }
    private void OnTriggerExit(Collider collider)
    {
        var touchable = collider.GetComponentInParent<ITouchable>();
        if (touchable != null) touchable.Deactivate(this);
    }

    public void ToTerminalMode(Transform camera)
    {
        if (playerState == PlayerState.InTerminal) return;
        playerState = PlayerState.InTerminal;
        cameraController.enabled = false;
        playerController.enabled = false;
        _prevPosition = cameraController.transform.position;
        _prevRotation = cameraController.transform.rotation;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(cameraController.transform.DOMove(camera.transform.position, 2));
        sequence.Join(cameraController.transform.DORotateQuaternion(camera.transform.rotation, 2));
        sequence.Play();
    }

    public void ToWorldMode()
    {
        if (playerState == PlayerState.InWorld) return;
        playerState = PlayerState.InWorld;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(cameraController.transform.DOMove(_prevPosition, 2));
        sequence.Join(cameraController.transform.DORotateQuaternion(_prevRotation, 2));
        sequence.Play().OnComplete(() =>
        {
            cameraController.enabled = true;
            playerController.enabled = true;
        });
    }
}
