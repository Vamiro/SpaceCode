using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class StartUp : BehaviourSingleton<StartUp>
{
    [SerializeField] private AudioMixer _audioMixer;
    public AudioMixer AudioMixer => _audioMixer;

    void Start()
    {
        StateMachine.Instance.ChangeState(new InitState());
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Initialize()
    {
        if (!SceneManager.GetSceneByBuildIndex(0).isLoaded)
        {
            SceneManager.LoadScene(0);
        }
    }
}
