using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class StartUp : BehaviourSingleton<StartUp>
{
    [SerializeField] private AudioMixer _audioMixer;
    public AudioMixer AudioMixer => _audioMixer;

    void Start()
    {
        StateMachine.Instance.ChangeState(new InitState());
    }
}
