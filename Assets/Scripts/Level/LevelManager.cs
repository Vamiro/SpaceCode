using System.Collections;
using System.Collections.Generic;
using MG_BlocksEngine2.Core;
using UnityEngine;

namespace Level
{
    public class LevelManager : BehaviourSingleton<LevelManager>
    {
        private List<GlowObjects> _glowObjects = new();
        private List<IControllPoint> _controlPoints = new();
        private int counter; 
        [SerializeField]private TargetObjectBehaviour targetObject;
        public Terminal currentTerminal;
        private bool _isFinished;

        public bool IsFinished => _isFinished;
        public TargetObjectBehaviour TargetObjectBehaviour => targetObject;
        public List<IControllPoint> ControlPoints => _controlPoints;

        protected override void Awake()
        {
            base.Awake();
            _glowObjects.AddRange(GetComponentsInChildren<GlowObjects>());
            _controlPoints.AddRange(GetComponentsInChildren<IControllPoint>());
            counter = _glowObjects.Count;
        }

        public void LevelPassed()
        {
            currentTerminal.isFinished = true;
            _isFinished = true;
            BE2_MainEventsManager.Instance.TriggerEvent(BE2EventTypes.OnStop);
            StartCoroutine(OnGlowObjects());
        }

        private IEnumerator OnGlowObjects()
        {
            for (int i = counter - 1; i >= 0; i--)
            {
                _glowObjects[i].OnGlowing();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
