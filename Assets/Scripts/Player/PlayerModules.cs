using System;

[Flags]
public enum PlayerModules
{
    Anything = 1<<0,
    SlideModule = 1<<1,
    TurnModule = 1<<2,
    JumpModule = 1<<3,
    ChangeColorModule = 1<<4,
}