using System;
using MG_BlocksEngine2.Block.Instruction;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MG_BlocksEngine2.Core;
using Unity.VisualScripting;
using UnityEngine;

public class BE2_Async_Instruction : BE2_InstructionBase
{
    private CancellationTokenSource _cts;

    protected override void OnButtonStop()
    {
        base.OnButtonStop();
        if (_cts != null)
        {
            _cts.Cancel();
            _cts = null;
        }
    }

    public new async void Function()
    {
        try
        {
            if (_cts != null)
            {
                //Debug.LogError($"Async instruction {name} already executed", this);
                return; //stops method if Function already exists on this object
            }

            _cts = new CancellationTokenSource(); //create a Cancellation token
            var result = await ExecuteFunction(_cts.Token); //execute current block
            if (result && _cts != null)
            {
                _cts = null;
                Debug.Log($"Execute next instruction{this}");
                ExecuteNextInstruction();
            }
            else
            {
                _cts = null;
                BE2_ExecutionManager.Instance.Stop();
            }
        }
        catch (TaskCanceledException)
        {
            //ignore
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            _cts = null;
            throw;
        }
    }

    protected virtual Task<bool> ExecuteFunction(CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}