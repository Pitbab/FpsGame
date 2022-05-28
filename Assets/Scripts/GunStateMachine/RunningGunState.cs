using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningGunState : GunState
{
    public RunningGunState(StateMachine stateMachine, string animationBool, TempContoller controller) : base(stateMachine, animationBool, controller) {}
    private bool isHolding;
    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        HandleInput();
        
        if (isHolding) return;
        stateMachine.ChangeState(controller.idleGunState);
    }

    public override void HandleInput()
    {
        base.HandleInput();
        isHolding = Input.GetKey(KeyCode.LeftShift);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
