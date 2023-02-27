using MG_BlocksEngine2.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelItemBehaviour : MonoBehaviour
{
     
    private int _myIndexInItemList;

    private void Awake()
    {
        _myIndexInItemList = LevelItemManager.Instance.AddItemToItemList(transform);

        BE2_MainEventsManager.Instance.StartListening(BE2EventTypes.OnStop, ResetPosition);
    }

    private void ResetPosition()
    {
        LevelItemManager.Instance.ResetItemByIndex(_myIndexInItemList, transform);
    }
}
