using MG_BlocksEngine2.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    List<GlowObjects> glowObjects = new List<GlowObjects>();
    private IEnumerator _finishAnimation;

    public bool _isFinished = false;

    static LevelManager _instance;
    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LevelManager>() as LevelManager;
            }
            return _instance;
        }
        set => _instance = value;
    }

    private void Awake()
    {
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            glowObjects.Add(transform.GetChild(i).GetComponent<GlowObjects>());
        }
        _finishAnimation = OnGlowObjects();
    }

    public void LevelPassed()
    {
        _isFinished = true;
        BE2_MainEventsManager.Instance.TriggerEvent(BE2EventTypes.OnStop);
        StartCoroutine(_finishAnimation);
    }

    IEnumerator OnGlowObjects()
    {
        for(int i = glowObjects.Count - 1; i >= 0; i--)
        {
            StartCoroutine(glowObjects[i].coroutineOnGlow);
            yield return new WaitForSeconds(0.2f);
        }
        StopCoroutine(_finishAnimation);
    }
}
