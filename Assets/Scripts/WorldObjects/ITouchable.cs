using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchable
{
    Vector3 ObjectPosition { get; }
    void Activate(PlayerBehaviour playerBehaviour);
    void Deactivate();
    
    void EnableOutline(bool isEnabled);
}
