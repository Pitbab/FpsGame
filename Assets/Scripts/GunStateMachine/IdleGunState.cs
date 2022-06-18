using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleGunState : GunState
{
    public IdleGunState(StateMachine stateMachine, string animationBool, TempContoller controller, GunData gunData) : base(stateMachine, animationBool, controller, gunData) {}
    public override void Enter()
    {
        base.Enter();

    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void Update()
    {
        base.Update();

        if (isAiming)
        {
            controller._animator.Play("IdleAim");
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            stateMachine.ChangeState(controller.simpleReloadState);
        }

        if (isShooting)
        {
            stateMachine.ChangeState(controller.singleFireState);
        }

        if (isRunning && !isAiming)
        {
            stateMachine.ChangeState(controller.runningGunState);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }
}
