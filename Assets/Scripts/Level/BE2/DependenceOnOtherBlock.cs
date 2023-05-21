using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependenceOnOtherBlock : MonoBehaviour
{
    [SerializeField] private GameObject _dependenceBlock;

    private void Start()
    {
        if (_dependenceBlock.activeSelf) gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }
}
