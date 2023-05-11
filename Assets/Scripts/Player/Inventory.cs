using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData
{
    public PlayerModules playerModules;
}


public class Inventory : MonoBehaviour, IStorable<InventoryData>
{
    [SerializeField]private PlayerModules _playerModules;

    public PlayerModules Get => _playerModules;

    public void Set(PlayerModules moduleToSet)
    {
        _playerModules |= moduleToSet;
    }

    public void Unset(PlayerModules moduleToUnset)
    {
        _playerModules &= ~moduleToUnset;
    }

    [SerializeField] private string _id = Guid.NewGuid().ToString();
    public string Id => _id;
    public void LoadData(InventoryData data)
    {
        if (data == null) {
            // Base data
        }
        else {
            _playerModules = data.playerModules;
        }
    }

    public InventoryData SaveData()
    {
        return new InventoryData()
        {
            playerModules = _playerModules
        };
    }
}