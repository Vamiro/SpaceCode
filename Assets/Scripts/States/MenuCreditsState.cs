using DG.Tweening;
using UnityEngine;

public class MenuCreditsState : IState
{
    public MenuCredits MenuCredits => Panels.Instance.menuCredits;
    private RectTransform _menuCreditsRect;

    public void Enter()
    {
        MenuCredits.onBack = OnBack;
        MenuCredits.Show();

        _menuCreditsRect = MenuCredits.gameObject.GetComponent<RectTransform>();
        _menuCreditsRect.DOAnchorPos(new Vector2(250, 0), 0.5f);
    }

    private void OnBack()
    {
        StateMachine.Instance.ChangeState(new MainMenuState());
    }

    public void Exit()
    {
        _menuCreditsRect.DOAnchorPos(new Vector2(2500, 0), 0.5f).OnComplete(() => MenuCredits.Close());
    }
}
