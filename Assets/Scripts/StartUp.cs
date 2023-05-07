using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class StartUp : BehaviourSingleton<StartUp>
{
    [SerializeField] private AudioMixer _audioMixer;
    public AudioMixer AudioMixer => _audioMixer;

    public GameObject camera;

    private void Start()
    {
        StateMachine.Instance.ChangeState(new InitState());
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize()
    {
        if (SceneManager.GetSceneByBuildIndex(0).isLoaded) return;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        asyncOperation.completed += _ => StateMachine.Instance.ChangeState(new InitState());
    }
}
