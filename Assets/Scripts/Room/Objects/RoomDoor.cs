using DG.Tweening;
using UnityEngine;

public class RoomDoor : MonoBehaviour, IButton
{
    [SerializeField] private GameObject _door;
    private Collider _doorBlock;
    private float _x, _y, _z;
    private readonly float _open = -2, _close = 0;
    bool isOn = false;
    Transform _doorTransform;
    
    private void Awake()
    {
        _doorTransform = _door.transform;
        _doorBlock = transform.GetComponent<BoxCollider>();
        
        var localPosition = _doorTransform.localPosition;
        _x = localPosition.x;
        _z = localPosition.z;
    }
    private void OpenDoor()
    {
        if (Mathf.Round(_doorTransform.localPosition.y) != Mathf.Round(_open))
        {
            _doorBlock.enabled = false;
            _doorTransform.DOLocalMoveY(_open, 2).OnComplete(() => SetPosition(_x, _open, _z));
        }
        else
        {
            _doorBlock.enabled = true;
            _doorTransform.DOLocalMoveY(_close, 2).OnComplete(() => SetPosition(_x, _close, _z));
        }
    }

    private void SetPosition(float x, float y, float z)
    {
        _doorTransform.localPosition = new Vector3(x, y, z);
        isOn = false;
    }

    public void ButtonPressed()
    {
        if (!isOn) OpenDoor();
    }
}
