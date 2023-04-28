using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class GameData
{
    public List<StoreItem> Items = new();

    public GameData()
    {
        
    }
}

[Serializable]
public struct StoreItem
{
    public string Id;
    public string Value;
}