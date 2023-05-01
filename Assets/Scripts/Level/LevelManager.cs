using System.Collections;
using System.Collections.Generic;
using MG_BlocksEngine2.Core;
using UnityEngine;

namespace Level
{
    public class LevelManager : BehaviourSingleton<LevelManager>
    {
        private List<GlowObjects> glowObjects = new();
        private int counter; 
        [SerializeField]private TargetObjectBehaviour targetObject;
        public Terminal currentTerminal;
        private bool _isFinished;

        public bool IsFinished => _isFinished;
        public TargetObjectBehaviour TargetObjectBehaviour => targetObject;

        protected override void Awake()
        {
            base.Awake();
            glowObjects.AddRange(GetComponentsInChildren<GlowObjects>());
            counter = glowObjects.Count;
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
                glowObjects[i].OnGlowing();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
