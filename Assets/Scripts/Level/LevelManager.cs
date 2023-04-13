using System.Collections.Generic;
using MG_BlocksEngine2.Core;
using UnityEngine;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        List<GlowObjects> glowObjects = new List<GlowObjects>();
        public bool _isFinished = false;
        private int _counter;
        public static LevelManager Instance { get; private set; }
        public TargetObjectBehaviour targetObject;
    
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            for (int i = 0; i < transform.childCount - 1; i++)
            {
                var component = transform.GetChild(i).GetComponent<GlowObjects>();
                if (component != null) glowObjects.Add(component);
            }
            _counter = glowObjects.Count - 1;
        }

        public void LevelPassed()
        {
            _isFinished = true;
            BE2_MainEventsManager.Instance.TriggerEvent(BE2EventTypes.OnStop);
            Invoke("OnGlowObjects", 1f);
        }

        private void OnGlowObjects()
        {
            if (_counter == -1)
            {
                _counter = glowObjects.Count - 1;
                return;
            }
            else
            {
                glowObjects[_counter].Invoke("OnGlowing", 0f);
                _counter--;
                Invoke("OnGlowObjects", 0.1f);
            }
        }

        private void OnDestroy()
        {
            if(Instance == this) Instance = null;
        }
    }
}
