using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Update = Unity.VisualScripting.Update;

public class StateMachine : BehaviourSingleton<StateMachine>
{
    public bool IsGameOn { get; set; }

    public IState GetCurrentState { get; private set; }
    public IState PrevState { get; private set; }
    
    public string NextState { get; private set; }

    private void Update()
    {
        GetCurrentState.HandleInput();
    }

    public void ChangeState(IState state)
    {
        NextState = state.GetType().Name;
        //Debug.Log($"Before enter\nPrevState - {PrevState}\nCurrentState - {GetCurrentState} state - {state}\nNextState - {NextState}");
        if (GetCurrentState != null && GetCurrentState.GetType().Name == state.GetType().Name)
        {
            Debug.Log($"Can't enter same state {state} twice");
            return;
        }
        if (GetCurrentState != null)
        {
            PrevState = GetCurrentState;
            GetCurrentState.Exit();
        }
        GetCurrentState = state;
        if (GetCurrentState != null)
        {
            GetCurrentState.Enter();
        }
    }
}
