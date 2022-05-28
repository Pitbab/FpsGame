using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleGunState : GunState
{
    public IdleGunState(StateMachine stateMachine, string animationBool, TempContoller controller) : base(stateMachine, animationBool, controller) {}
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

    public override void Exit()
    {
        base.Exit();
    }
}
