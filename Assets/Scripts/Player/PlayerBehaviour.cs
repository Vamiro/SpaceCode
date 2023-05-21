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
    private HashSet<ITouchable> _touchableObj = new HashSet<ITouchable>();
    public Terminal currentTerminal;
 
    public Inventory GetInventory => this.GetComponent<Inventory>();

    public ITouchable ActivateNearestTerminalOrButton()
    {
        var obj = FindNearestObject();
        if (obj != null) return obj;
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

    private ITouchable FindNearestObject()
    {
        return _touchableObj.OrderBy((obj) => Vector3.Distance(obj.ObjectPosition,
            transform.position)).FirstOrDefault();
    }

    private void HandleTouchableObject(ITouchable obj, bool isEnter)
    {
        if (obj != null)
        {
            if (isEnter)
            {
                _touchableObj.Add(obj);
            }
            else
            {
                _touchableObj.Remove(obj);
            }
            obj.EnableOutline(isEnter);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        var obj = collider.GetComponentInParent<ITouchable>();
        HandleTouchableObject(obj, true);
    }

    private void OnTriggerExit(Collider collider)
    {
        var obj = collider.GetComponentInParent<ITouchable>();
        HandleTouchableObject(obj, false);
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
