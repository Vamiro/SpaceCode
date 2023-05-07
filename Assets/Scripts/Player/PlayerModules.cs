using System;

[Flags]
public enum PlayerModules
{
    Anything = 1<<0,
    TurnModule = 1<<1,
    JumpModule = 1<<2,
    ChangeColorModule = 1<<3,
}