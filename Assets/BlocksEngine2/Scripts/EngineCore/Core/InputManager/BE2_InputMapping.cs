using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BE2_InputMapping : MonoBehaviour
{


    public struct PrimaryKeyT
    {
        public bool Hold => Input.GetKey(KeyCode.A);
    }

    public PrimaryKeyT PrimaryKey = new PrimaryKeyT();

    public struct aa
    {
        public bool Hold;
    }

    public aa aaa;
}