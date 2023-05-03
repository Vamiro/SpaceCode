using UnityEngine.SceneManagement;

public class LoadingSceneState: IState
{
    public LoadingScreen LoadingScreen => Panels.Instance.loadingScreen;
    private string _scene;
    private IState _state;
    private LoadSceneMode _loadMode;

    public string Scene => _scene;
    public LoadSceneMode LoadMode => _loadMode;

    public LoadingSceneState(IState state, string strScene = null, LoadSceneMode loadMode  = LoadSceneMode.Additive)
    {
        if (strScene != null)
        {
            _scene = "Scenes/" + strScene;
        }
        _state = state;
        _loadMode = loadMode;
    }

    public void Enter()
    {
        LoadingScreen.Show(this);
    }

    public void Exit()
    {
        LoadingScreen.Close();
    }

    public void OnLoadingCompleted()
    {
        StateMachine.Instance.ChangeState(_state);
    }

    public void HandleInput()
    {
    }
}