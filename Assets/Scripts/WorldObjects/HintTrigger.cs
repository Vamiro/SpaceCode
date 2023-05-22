using System;
using System.Collections;
using UnityEngine;

public class HintTrigger : MonoBehaviour
{
    private bool isPlayerInside;
    [SerializeField] private bool IsOn;
    [SerializeField] private HintMode _hintMode;
    [SerializeField] private int _hintNumber;

    private void Awake()
    {
        if (!IsOn)
        {
            this.enabled = false;
        }
    }

    private void Start()
    {
        if ((_hintMode & HintMode.OnStart) != 0)
        {
            float delay = 1.0f;
            StartCoroutine(DelayedStartCoroutine(delay));
        }
    }
    
    IEnumerator DelayedStartCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        StateMachine.Instance.ChangeState(new HintState(_hintNumber));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            if ((_hintMode & HintMode.OnTrigger) != 0)
            {
                StateMachine.Instance.ChangeState(new HintState(_hintNumber));
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && ((_hintMode == HintMode.ActivateOnTrigger && isPlayerInside) || (_hintMode & HintMode.Activate) != 0))
        {
            StateMachine.Instance.ChangeState(new HintState(_hintNumber));
        }
    }
}

[Flags]
public enum HintMode
{
    Activate = 1<<0,
    OnTrigger = 1<<1,
    ActivateOnTrigger = 1<<2,
    OnStart = 1<<3,
}
