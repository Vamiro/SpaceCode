using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Update = Unity.VisualScripting.Update;

public class StateMachine : BehaviourSingleton<StateMachine>
{
    public bool IsGameOn { get; private set; }

    public IState GetCurrentState { get; private set; }
    public IState PrevState { get; private set; }

    private void Update()
    {
        GetCurrentState.HandleInput();
    }

    public void ChangeState(IState state)
    {
        if (GetCurrentState != null)
        {
            Debug.Log($"Exit form state {GetCurrentState.GetType().Name}");
            PrevState = GetCurrentState;
            GetCurrentState.Exit();
        }
        GetCurrentState = state;
        if (GetCurrentState != null)
        {
            Debug.Log($"Enter state {GetCurrentState.GetType().Name}");
            GetCurrentState.Enter();
            if (!IsGameOn && GetCurrentState.GetType().Name == "GameOnState")
            {
                IsGameOn = true;
            }
        }
    }
}
