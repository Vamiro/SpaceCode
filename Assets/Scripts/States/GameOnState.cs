using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOnState :  IState
{
    private PlayerBehaviour _player;
    private string _save;

    public GameOnState(string save = null)
    {
        _save = save;
    }

    public void Enter()
    {
        FindPlayerAndSetupPlayerBehaviour();
        if (!StateMachine.Instance.IsGameOn)
        {
            StateMachine.Instance.IsGameOn = true;
            StateMachine.Instance.ChangeState(new HintState(1));
        }
    }

    public void Exit()
    {
        if (StateMachine.Instance.NextState != "HintState")
        {
            _player.ExitGameMode();
        }
    }

    private void FindPlayerAndSetupPlayerBehaviour()
    {
        var player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            _player = player.GetComponent<PlayerBehaviour>();
            _player.EnterGameMode();
            if (_save != null)
            {
                _player.ExitGameMode();
                StoreDataManager.Instance.LoadGame(_save);
            }
            MainMenuState.Menu.Close();
            StartUp.Instance.camera.SetActive(false);
            _player.EnterGameMode();
        }
        else
        {
            throw new System.Exception("Player not found");
        }
    }

    public void HandleInput()
    {
        if (Input.GetButtonUp("Activate"))
        {
            var obj = _player.ActivateNearestTerminalOrButton();
            if (obj != null)
            {
                obj.Activate(_player);
            }
        }
        else if (Input.GetButtonUp("Esc"))
        {
            StateMachine.Instance.ChangeState(new MainMenuState());
            StartUp.Instance.camera.SetActive(true);
        }
    }
}
