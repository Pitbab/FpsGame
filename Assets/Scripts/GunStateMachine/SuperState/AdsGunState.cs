using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// state used to set gun settings when aiming (fov, sway, gun position)
public class AdsGunState : GunState
{
    public AdsGunState(StateMachine stateMachine, string animationBool, TempContoller controller, GunData gunData) : base(stateMachine, animationBool, controller, gunData) {}
    public override void Enter()
    {
        base.Enter();

        //controller.SetAimPos();
        //controller.transform.localPosition = gunData.AimingPos;
        //controller.SetFov(gunData.AimingFov);
        //controller.SetSway(false);
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
