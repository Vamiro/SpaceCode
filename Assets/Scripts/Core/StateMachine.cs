using UnityEngine;
public class StateMachine : Singleton<StateMachine>
{
    private IState _current;
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
        }
    }

}
