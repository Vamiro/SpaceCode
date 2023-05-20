using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControllPoint : MonoBehaviour, IControllPoint
{
    public bool IsPassed { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TargetObject"))
        {
            IsPassed = true;
        }
    }

    public void ResetControllPoint()
    {
        IsPassed = false;
    }
}
