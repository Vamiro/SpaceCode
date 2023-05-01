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
        if (!StateMachine.Instance.IsGameOn)
        {
            var sceneOperation = SceneManager.LoadSceneAsync("Scenes/TheFirstRoom", LoadSceneMode.Additive);
            sceneOperation.completed += _ =>
            {
                FindPlayerAndSetupPlayerBehaviour();
            };
        }
        else
        {
            FindPlayerAndSetupPlayerBehaviour();
        }
    }

    public void Exit()
    {
        _player.ExitGameMode();
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
            var terminal = _player.ActivateNearestTerminalOrButton();
            if (terminal != null)
            {
                StateMachine.Instance.ChangeState(new TerminalState(terminal));
            }
        }
        else if (Input.GetButtonUp("Esc"))
        {
            StateMachine.Instance.ChangeState(new MainMenuState());
            StartUp.Instance.camera.SetActive(true);
        }
    }
}
