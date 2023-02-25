using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInputMapping : BE2_InputMapping
{
    public new struct PrimaryKeyT
    {
        public bool Hold => Input.GetKey(KeyCode.B);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
