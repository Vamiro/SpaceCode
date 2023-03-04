using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "TargetObject")
        {
            LevelManager.Instance.LevelPassed();
        }
    }
}
