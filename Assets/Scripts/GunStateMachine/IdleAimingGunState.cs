using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// no breathing animation in this state
public class IdleAimingGunState : AdsGunState
{
    public IdleAimingGunState(StateMachine stateMachine, string animationBool, TempContoller controller, GunData gunData) : base(stateMachine, animationBool, controller, gunData) {}
    public override void Enter()
    {
        base.Enter();

    }

    public override void Update()
    {
        base.Update();
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            stateMachine.ChangeState(controller.simpleReloadState);
        }

        if (Input.GetMouseButtonDown(0))
        {
            stateMachine.ChangeState(controller.singleFireState);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            stateMachine.ChangeState(controller.runningGunState);
        }

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
