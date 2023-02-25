using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BE2_InputMappingDefault : BE2_InputMapping
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PrimaryKey.Hold);
        Debug.Log(GetComponent<TestInputMapping>().PrimaryKey.Hold);
    }
}
