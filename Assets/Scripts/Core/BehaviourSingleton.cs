using UnityEngine;

public class BehaviourSingleton<T> : MonoBehaviour where T : BehaviourSingleton<T>, new()
{
    private static T _instance;
    public static T Instance => _instance;

    protected virtual void Awake()
    {
        Debug.Assert(Instance == null);
        _instance = this as T;
    }
}
