using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Update = Unity.VisualScripting.Update;

public class StateMachine : BehaviourSingleton<StateMachine>
{
    private IState _current;
    private bool _isGameOn;
    
    public bool IsGameOn => _isGameOn;

    private void Update()
    {
        _current.HandleInput();
    }

    public void ChangeState(IState state)
    {
        if (_current != null)
        {
            Debug.Log($"Exit form state {_current.GetType().Name}");
            _current.Exit();
        }
        _current = state;
        if (_current != null)
        {
            Debug.Log($"Enter state {_current.GetType().Name}");
            _current.Enter();
            if (!_isGameOn && _current.ToString() == "GameOnState")
            {
                _isGameOn = true;
            }
        }
        
    }
}
