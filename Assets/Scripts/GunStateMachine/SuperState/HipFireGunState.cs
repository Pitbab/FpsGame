using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipFireGunState : GunState
{
    public HipFireGunState(StateMachine stateMachine, string animationBool, TempContoller controller) : base(stateMachine, animationBool, controller) {}
    public override void Enter()
    {
        base.Enter();
        controller.transform.localPosition = Vector3.zero;
        controller.SetFov(controller.normalFov);
        controller.SetSway(true);
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButton(1))
        {
            stateMachine.ChangeState(controller.idleAimingGunState);
        }
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void FixedUpdated()
    {
        base.FixedUpdated();
    }
}
