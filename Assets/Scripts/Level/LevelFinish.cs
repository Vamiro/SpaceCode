    using System;
using System.Collections.Generic;
using System.Linq;
using MG_BlocksEngine2.Core;
using UnityEngine;

namespace Level
{
    public class LevelFinish : MonoBehaviour
    {
        private void Awake()
        {
            BE2_MainEventsManager.Instance.StartListening(BE2EventTypes.OnStop, Reset);
        }

        private void OnTriggerEnter(Collider other)
        {
            
            if (other.CompareTag("TargetObject"))
            {
                if(CheckControllPoints()) LevelManager.Instance.LevelPassed();
            }
        }

        private bool CheckControllPoints()
        {
            var controlPoints = LevelManager.Instance.ControlPoints;
            Debug.Log(controlPoints.Count);
            if (controlPoints.Count > 0){
                foreach (var point in controlPoints)
                {
                    if (!point.IsPassed)
                    {
                        return false;
                    }
                    point.ResetControllPoint();
                }
            }
            return true;
        }

        private void Reset()
        {
            var controlPoints = LevelManager.Instance.ControlPoints;
            if (controlPoints.Count > 0){
                foreach (var point in controlPoints)
                {
                    point.ResetControllPoint();
                }
            }
        }

        private void OnDestroy()
        {
            BE2_MainEventsManager.Instance.StopListening(BE2EventTypes.OnStop, Reset);
        }
    }
}
