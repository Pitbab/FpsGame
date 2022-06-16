using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsGunState : GunState
{
    public AdsGunState(StateMachine stateMachine, string animationBool, TempContoller controller) : base(stateMachine, animationBool, controller) {}
    public override void Enter()
    {
        base.Enter();
        
        controller.SetAimPos();
        controller.SetFov(controller.aimFov);
        controller.SetSway(false);
    }

    public override void Update()
    {
        base.Update();
        if (!Input.GetMouseButton(1))
        {
            stateMachine.ChangeState(controller.idleGunState);
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
