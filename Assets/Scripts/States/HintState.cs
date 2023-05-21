using UnityEngine;

public class HintState: IState
{
    private HintPanel _hintPanel = Panels.Instance.hintPanel;
    private int _hintNumber;

    public HintState(int hintNumber)
    {
        _hintNumber = hintNumber;
    }
    
    public void Enter()
    {
        _hintPanel.onBack = OnBack;
        if (_hintNumber >= 0)
        {
            _hintPanel.Show(_hintNumber);
        }
        else
        {
            StateMachine.Instance.ChangeState(StateMachine.Instance.PrevState);
        }
    }

    private void OnBack()
    {
        StateMachine.Instance.ChangeState(StateMachine.Instance.PrevState);
    }

    public void Exit()
    {
        _hintPanel.Close();
    }

    public void HandleInput()
    {
        if (Input.GetButtonUp("Esc"))
        {
            StateMachine.Instance.ChangeState(StateMachine.Instance.PrevState);
        }
    }
}