using System;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MG_BlocksEngine2.Block;
using MG_BlocksEngine2.Block.Instruction;
using UnityEngine;

public class BE2_Ins_SlideForward : BE2_Async_Instruction, I_BE2_Instruction
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
    private float _value;
    private float _absValue;

    /*public override void OnStackActive()
    {
    }*/

    private Vector3 _initialPosition;
    private TweenerCore<Vector3, Vector3, VectorOptions> _tween;

    protected override async Task<bool> ExecuteFunction(CancellationToken cancellationToken)
    {
        _input0 = Section0Inputs[0];
        _value = _input0.FloatValue;
        _absValue = Mathf.Abs(_value);
        _initialPosition = TargetObject.Transform.position;

        if (_value <= 0)
        {
            Debug.LogError("Value cannot be negative");
            return false;
        }

        for (int i = 1; i <= _value; i++)
        {
            var isOut = await TryStep(_initialPosition + TargetObject.Transform.forward * i, cancellationToken);
            if (!isOut) return false;
        }

        await Task.Delay(TimeSpan.FromSeconds(3f), cancellationToken);
        return true;
    }

    protected async Task<bool> TryStep(Vector3 nextPos, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested) return false; //stops async method if needed
        
        var transformPosition = nextPos - TargetObject.Transform.position;
        bool killDate = false;
            //raycast starts
        if (Physics.Raycast(TargetObject.Transform.position, transformPosition, transformPosition.magnitude, ~(1 << LayerMask.NameToLayer("Ignore Raycast"))))
        {
            await Task.Delay(TimeSpan.FromSeconds(1.5f), cancellationToken);
            return false; //stops async method if collides with smth except layerMask "Ignore Raycast"
        }
            //raycast ends
        var tween = TargetObject.Transform.DOMove(nextPos, 0.2F).SetEase(Ease.Linear).SetAutoKill(false); //create DOTween var
        tween.OnUpdate(() => {if (cancellationToken.IsCancellationRequested) tween.Kill();}); //stops async method and kill DOTween var if needed
        tween.OnKill(() => { killDate = true; tween = null; Debug.Log($"Tween has been killed{this}");}); //lambda function witch starts when DOTween var was Killed
        await Task.WhenAny(tween.AsyncWaitForCompletion(), tween.AsyncWaitForKill()); //await fot Completion ot Killing DOTween var
        try
        {
            return !killDate && tween.IsComplete(); //returns True if DOTween var completed or False if killed
        }
        finally
        {
            if(!killDate) tween.Kill(); //Kills DOTween var if it completed
        }
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