using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;

public class CommandPlate : ScriptableObject
{
    // Start, Input>>, Output>, SomeMethod, "Var Type", If, If else, If elseIf else, for, while, arrays, foreach?
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ SomeMethod(Var Type) return (Var Type) ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    GameObject view;
}
/*
public interface ICommand
{
    CommandPlate ConfigData { get;   set; }
    ICommand Parent { get;  }
    ICommand PrevCommand { get;  }
    ICommand NextCommand { get;  }

    bool HasInput
    bool HasOutput
}


public EntryPointCommand : ICommand
{
    ICommand NextCommand { get;  }
}

public MethodCommand : ICommand
{
    MethodCommand(MethodInfo method) {

    }

}

Method
    config
        (input) value
            {
                for (input)
                    nextCommand? => (input) value?? => nextCommand(true input) : task solved => do smth
                    }
            }
        (output)

Input prevCommand, playerInput.

start
{ }
method output <("Введите пароль")>
{ }
method input <(int)>
{ 
    wait for playerInput;
    nextCommand(playerInput);
}
method OpenDoor <("Если пароль верный, то отрыть дверь")>
{
    if(playerInput == password){
        solveTask();
    }
}

Solve(){
    openDoor(){
        door.transform;
    }
}




public interface IValue
{
    string Name;
    object GetValue();
    void SetValue(object o);
}


// Arg: Variable, Constant, Expression

*/