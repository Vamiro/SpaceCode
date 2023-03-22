using DG.Tweening;
using UnityEngine;

public class RoomDoor : MonoBehaviour, IButton
{
    [SerializeField] private Collider _doorBlock;
    private float _x, _y, _openY, _z;
    bool isOn = false;

    private void Awake()
    {
        _x = transform.position.x;
        _y = transform.position.y;
        _openY = Mathf.Abs(transform.position.y) * -2;
        _z = transform.position.z;
    }
    private void OpenDoor()
    {
        if (Mathf.Round(transform.position.y) != Mathf.Round(_openY))
        {
            _doorBlock.enabled = false;
            transform.DOMove(new Vector3(_x, _openY, _z), 2).OnComplete(() => SetPosition(_x, _openY, _z));
        }
        else
        {
            _doorBlock.enabled = true;
            transform.DOMove(new Vector3(_x, _y, _z), 2).OnComplete(() => SetPosition(_x, _y, _z));
        }
    }

    private void SetPosition(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
        isOn = false;
    }

    public void ButtonPressed()
    {
        if (!isOn) OpenDoor();
    }
}
