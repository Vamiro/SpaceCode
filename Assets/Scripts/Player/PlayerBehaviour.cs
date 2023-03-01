using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    CameraController cameraController;
    PlayerController playerController;
    private Vector3 _prevPosition;
    private Quaternion _prevRotation;

    private enum PlayerState { InWorld, InTerminal }
    private PlayerState playerState = PlayerState.InWorld;

    [SerializeField] private GameObject _world;
    [SerializeField] private Camera _camera;

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
        var sceneOperation = SceneManager.LoadSceneAsync("Scenes/TerminalScreen", LoadSceneMode.Additive);
        sceneOperation.completed += (e) =>
        {
            _world.SetActive(false);
            cameraController.enabled = false;
            playerController.enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            _camera.enabled = false;
        };
        /*_prevPosition = cameraController.transform.position;
        _prevRotation = cameraController.transform.rotation;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(cameraController.transform.DOMove(camera.transform.position, 2));
        sequence.Join(cameraController.transform.DORotateQuaternion(camera.transform.rotation, 2));
        sequence.Play();*/
    }

    public void ToWorldMode()
    {
        if (playerState == PlayerState.InWorld) return;
        playerState = PlayerState.InWorld;
        var sceneOperation = SceneManager.UnloadSceneAsync("Scenes/TerminalScreen");
        sceneOperation.completed += (e) => {
            _world.SetActive(true);
            GetComponent<MeshRenderer>().enabled = true;
            _camera.enabled = true;
            cameraController.enabled = true;
            playerController.enabled = true;
        };
        
        /*Sequence sequence = DOTween.Sequence();
        sequence.Append(cameraController.transform.DOMove(_prevPosition, 2));
        sequence.Join(cameraController.transform.DORotateQuaternion(_prevRotation, 2));
        sequence.Play().OnComplete(() =>
        {
            cameraController.enabled = true;
            playerController.enabled = true;
        });*/
    }
}
