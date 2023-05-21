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
        if (GetCurrentState != null)
        {
            PrevState = GetCurrentState;
            GetCurrentState.Exit();
        }
        GetCurrentState = state;
        if (GetCurrentState != null)
        {
            Debug.Log("Before enter\nPrevState - " + PrevState + "\nCurrentState - " + GetCurrentState + "\nNextState - " + NextState);
            GetCurrentState.Enter();
        }
        }
}
