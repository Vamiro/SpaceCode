using System;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Level;
using MG_BlocksEngine2.Block;
using MG_BlocksEngine2.Block.Instruction;
using UnityEngine;

public class BE2_Ins_Jump : BE2_Async_Instruction, I_BE2_Instruction
{
    //protected override void OnAwake()
    //{
    //
    //}

    //protected override void OnStart()
    //{
    //    
    //}

    private I_BE2_BlockSectionHeaderInput _input0;

    /*public override void OnStackActive()
    {
    }*/

    private Vector3 _initialPosition;
    private TweenerCore<Vector3, Vector3, VectorOptions> _tween;

    protected override async Task<bool> ExecuteFunction(CancellationToken cancellationToken)
    {
        _initialPosition = TargetObject.Transform.position;
        var isOut = await LevelManager.Instance.TargetObjectBehaviour.StepForward(
            _initialPosition + TargetObject.Transform.up, cancellationToken);
        
        if (!isOut) return false;
        
        _initialPosition = TargetObject.Transform.position;
        isOut = await LevelManager.Instance.TargetObjectBehaviour.StepForward(
            _initialPosition + TargetObject.Transform.forward, cancellationToken);
        
        if (!isOut || !LevelManager.Instance.TargetObjectBehaviour.CheckGround()) return false;
        
        return true;
    }
    /*public new void Function()
    {
        if (_firstPlay)
        {
            _input0 = Section0Inputs[0];
            _value = _input0.FloatValue;
            _absValue = Mathf.Abs(_value);
            _initialPosition = TargetObject.Transform.position;
            _firstPlay = false;
        }

        if (_counter < _absValue)
        {
            // v2.8 - adjusted the SlideForward function so the TargetObject always end in the same position
            if (_timer < 1)
            {
                _timer += Time.deltaTime / 0.2f;

                if (_timer > 1)
                    _timer = 1;
                var pos = Vector3.Lerp(_initialPosition, _initialPosition +
                                                         TargetObject.Transform.forward * (_value / _absValue), _timer);
                if (Physics.Raycast(TargetObject.Transform.position - TargetObject.Transform.forward / 2,
                        TargetObject.Transform.forward, 1f)) return;
                TargetObject.Transform.position = pos;
            }
            else
            {
                _timer = 0;
                _counter++;
                _firstPlay = true;
            }
        }
        else
        {
            ExecuteNextInstruction();
            _counter = 0;
            _timer = 0;
            _firstPlay = true;
        }
    }*/
}