using UnityEngine;

public class TerminalState : IState
{
    private Terminal _terminal;
    
    public TerminalState(Terminal terminal)
    {
        _terminal = terminal;
    }

    public void Enter()
    {
        if (StateMachine.Instance.PrevState.GetType().Name != "HintState")
        {
            _terminal.EnterTerminalMode();
        }
    }

    public void Exit()
    {
        if (StateMachine.Instance.NextState != "HintState")
        {
            _terminal.ExitTerminalMode();
        }
    }

    public void HandleInput()
    {
        if (Input.GetButtonUp("Esc"))
        {
            StateMachine.Instance.ChangeState(new GameOnState());
        }
    }
}
