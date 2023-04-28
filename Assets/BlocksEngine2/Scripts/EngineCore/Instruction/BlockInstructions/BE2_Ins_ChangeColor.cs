using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Level;
using MG_BlocksEngine2.Block;
using MG_BlocksEngine2.Block.Instruction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    //protected override void OnPlay()
    //{
    //    
    //}

    private I_BE2_BlockSectionHeaderInput _input0;
    private ColorNames _value;
    [SerializeField] private TMP_Dropdown _dropdown;

    protected override void OnAwake()
    {
        base.OnAwake();
        _dropdown.options.Clear();
        _dropdown.options.AddRange(Enum.GetNames(typeof(ColorNames)).Select((s => new TMP_Dropdown.OptionData(s))));
    }

    protected override async Task<bool> ExecuteFunction(CancellationToken cancellationToken)
    {
        _input0 = Section0Inputs[0];
        _value = _input0.StringValue.ColorName();
        var isOut = await  LevelManager.Instance.TargetObjectBehaviour.ChangeColor(_value, cancellationToken);
        return isOut;
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
