using DG.Tweening;
using MG_BlocksEngine2.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemStorage
{
    private Vector3 _position;
    private Quaternion _rotation;
    private Vector3 _scale;
    private Transform _transform;

    public ItemStorage(Transform transform)
    {
        _transform = transform;
        StoreItem();
    }

    public void StoreItem()
    {
        _position = _transform.position;
        _rotation = _transform.rotation;
        _scale = _transform.localScale;
    }

    public void ResetItem()
    {
        if (!LevelManager.Instance._isFinished)
        {
            _transform.position = _position;
            _transform.rotation = _rotation;
            _transform.localScale = _scale;
        }
    }
}