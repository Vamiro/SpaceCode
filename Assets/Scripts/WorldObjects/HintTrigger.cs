using System;
using UnityEngine;

public class HintTrigger : MonoBehaviour
{
    private bool isPlayerInside;
    [SerializeField] private HintMode _hintMode;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            if (_hintMode == HintMode.OnTrigger)
            {
                StateMachine.Instance.ChangeState(new HintState(0));
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && ((_hintMode == HintMode.ActivateOnTrigger && isPlayerInside) || _hintMode == HintMode.Activate))
        {
            StateMachine.Instance.ChangeState(new HintState(0));
        }
    }
}

public enum HintMode
{
    Activate,
    OnTrigger,
    ActivateOnTrigger,
}
