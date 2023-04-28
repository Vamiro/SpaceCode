using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOnState : IState
{
    private static bool _isGameOn;
    private PlayerBehaviour _player;

    private string _save;
    
    public static bool IsGameOn => _isGameOn;

    public GameOnState(string save = null)
    {
        _save = save;
    }

    public void Enter()
    {
        if (!_isGameOn)
        {
            var sceneOperation = SceneManager.LoadSceneAsync("Scenes/TheFirstRoom", LoadSceneMode.Additive);
            sceneOperation.completed += _ =>
            {
                LoadGameOnState();
            };
        }
        else
        {
            FindPlayerAndSetupPlayerBehaviour();
        }
    }

    private void LoadGameOnState()
    {
        FindPlayerAndSetupPlayerBehaviour();
        _player.OnPlayerStateChanged += HandlePlayerStateChanged;
        MainMenuState.Menu.onBack = () =>
        {
            StateMachine.Instance.ChangeState(new GameOnState());
        };
        _isGameOn = true;
    }

    private void FindPlayerAndSetupPlayerBehaviour()
    {
        var player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            _player = player.GetComponent<PlayerBehaviour>();
            if (_save != null)
            {
                _player.ExitGameMode(PlayerBehaviour.PlayerState.InMenu,false);
                StoreDataManager.Instance.LoadGame(_save);
                _player.EnterGameMode(false);
            }
            StartUp.Instance.camera.SetActive(false);
            MainMenuState.Menu.Close();
        }
        else
        {
            throw new System.Exception("Player not found");
        }
    }

    private void HandlePlayerStateChanged(PlayerBehaviour.PlayerState newState)
    {
        if (newState == PlayerBehaviour.PlayerState.InMenu)
        {
            StateMachine.Instance.ChangeState(new MainMenuState());
        }
        else if (newState == PlayerBehaviour.PlayerState.InWorld)
        {
            StateMachine.Instance.ChangeState(new GameOnState());
        }
    }

    
    public void Exit()
    {
        StartUp.Instance.camera.SetActive(true);
    }
}
