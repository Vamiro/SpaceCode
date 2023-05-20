using System.Collections;
using System.Collections.Generic;
using Level;
using UnityEngine;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;

public class BE2_CheckObstacle : BE2_InstructionBase, I_BE2_Instruction
{
    //protected override void OnAwake()
    //{
    //
    //}

    //protected override void OnStart()
    //{
    //    
    //}

    I_BE2_BlockSectionHeaderInput _input0;
    I_BE2_BlockSectionHeaderInput _input1;
    BE2_InputValues _v0;
    BE2_InputValues _v1;

    public new string Operation()
    {
        return LevelManager.Instance.TargetObjectBehaviour.CheckObstacle() ? "1" : "0";
    }
}
