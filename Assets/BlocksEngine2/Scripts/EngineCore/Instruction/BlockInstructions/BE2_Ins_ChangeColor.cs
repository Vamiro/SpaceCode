﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MG_BlocksEngine2.Block;
using MG_BlocksEngine2.Block.Instruction;
using UnityEngine;

public class BE2_Ins_ChangeColor : BE2_Async_Instruction, I_BE2_Instruction
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
    string _value;

    //protected override void OnPlay()
    //{
    //    
    //}

    
    protected override async Task<bool> ExecuteFunction(CancellationToken cancellationToken)
    {
        _input0 = Section0Inputs[0];
        _value = _input0.StringValue;

        Color newColor = Color.white;

        switch (_value)
        {
            case "Red":
                ColorUtility.TryParseHtmlString("#FF0000", out newColor);
                break;
            case "Orange":
                ColorUtility.TryParseHtmlString("#FF7F00", out newColor);
                break;
            case "Yellow":
                ColorUtility.TryParseHtmlString("#FFFF00", out newColor);
                break;
            case "Green":
                ColorUtility.TryParseHtmlString("#00FF00", out newColor);
                break;
            case "Blue":
                ColorUtility.TryParseHtmlString("#0000FF", out newColor);
                break;
            case "Violet":
                ColorUtility.TryParseHtmlString("#8B00FF", out newColor);
                break;
            default:
                break;
        }
        await Task.Delay(TimeSpan.FromSeconds(1f), cancellationToken);
        TargetObject.Transform.GetComponent<Renderer>().materials[0].SetColor("_Color", newColor);
        return true;
    }
    /*public new void Function()
    {
        _input0 = Section0Inputs[0];
        _value = _input0.StringValue;

        Color newColor = Color.white;

        switch (_value)
        {
            case "Red":
                ColorUtility.TryParseHtmlString("#FF0000", out newColor);
                break;
            case "Orange":
                ColorUtility.TryParseHtmlString("#FF7F00", out newColor);
                break;
            case "Yellow":
                ColorUtility.TryParseHtmlString("#FFFF00", out newColor);
                break;
            case "Green":
                ColorUtility.TryParseHtmlString("#00FF00", out newColor);
                break;
            case "Blue":
                ColorUtility.TryParseHtmlString("#0000FF", out newColor);
                break;
            case "Violet":
                ColorUtility.TryParseHtmlString("#8B00FF", out newColor);
                break;
            default:
                break;
        }

        TargetObject.Transform.GetComponent<Renderer>().materials[0].SetColor("_Color", newColor);
        ExecuteNextInstruction();
    }*/
}
