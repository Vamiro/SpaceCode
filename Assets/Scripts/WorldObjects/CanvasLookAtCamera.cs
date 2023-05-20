using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAtCamera : MonoBehaviour
{
    void Update()
    {
        if (StateMachine.Instance.GetCurrentState.GetType().Name == "GameOnState")
        {
            transform.LookAt(Camera.main.transform.position);
        }
    }
}
