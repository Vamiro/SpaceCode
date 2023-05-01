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
        _terminal.EnterTerminalMode();
    }

    public void Exit()
    {
        _terminal.ExitTerminalMode();
    }

    public void HandleInput()
    {
        if (Input.GetButtonUp("Esc"))
        {
            StateMachine.Instance.ChangeState(new GameOnState());
        }
    }
}
