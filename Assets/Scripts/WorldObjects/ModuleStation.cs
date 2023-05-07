using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleStation : MonoBehaviour, ITouchable
{
    public Vector3 ObjectPosition => transform.position;
    [SerializeField] private PlayerModules _playerModules;
    
    public void Activate(PlayerBehaviour playerBehaviour)
    {
        playerBehaviour.GetInventory.Set(_playerModules);
    }

    public void Deactivate()
    {
    }
    public void EnableOutline(bool isEnabled)
    {
    }
}
