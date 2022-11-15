using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchable
{
    void Activate(PlayerBehaviour player);
    void Deactivate(PlayerBehaviour player);
}
