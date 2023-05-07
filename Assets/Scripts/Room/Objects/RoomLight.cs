using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLight : MonoBehaviour, IObjectActivated
{
    public void ActivateObject()
    {
        gameObject.SetActive(true);
    }

    public void DeactivateObject()
    {
        
    }
}
