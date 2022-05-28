using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipFireGunState : GunState
{
    public HipFireGunState(StateMachine stateMachine, string animationBool, TempContoller controller) : base(stateMachine, animationBool, controller) {}

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
