using DG.Tweening;
using MG_BlocksEngine2.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemCharacteristics
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale;
    public ItemCharacteristics(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        Position = position;
        Rotation = rotation;
        Scale = scale;
    }
}

public class LevelItemManager : MonoBehaviour
{
    [SerializeField] private List<ItemCharacteristics> _levelItemsList = new List<ItemCharacteristics>();
    static LevelItemManager _instance;

    public static LevelItemManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LevelItemManager>();
            }
            return _instance;
        }
        set => _instance = value;
    }

    private void Awake()
    {
        
    }

    public int AddItemToItemList(Transform itemPosition)
    {
        _levelItemsList.Add(new ItemCharacteristics(itemPosition.position, itemPosition.rotation, itemPosition.localScale));
        return _levelItemsList.Count - 1;
    }

    public void ResetItemByIndex(int index, Transform transform)
    {
        transform.position = _levelItemsList[index].Position;
        transform.rotation = _levelItemsList[index].Rotation;
        transform.localScale = _levelItemsList[index].Scale;
    }
}
