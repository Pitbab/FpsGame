using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleFIreState : GunState
{
    private float totalTime;
    private float currentTime;
    private float switchTime;
    private float timeToAuto = 0.05f;
    private Ray trajectory;
    public SingleFIreState(StateMachine stateMachine, string animationBool, TempContoller controller, GunData gunData) : base(stateMachine, animationBool, controller, gunData) {}
    public override void Enter()
    {
        base.Enter();
        currentTime = 0f;
        totalTime = 0.1f;
        switchTime = 0.05f;
        controller.SetFlash(true);
        controller.PlaySingleShot();
        RayCastBullet();
        controller.Recoil();
        
        
    }

    public override void Update()
    {
        base.Update();
        
        if (totalTime < currentTime)
        {
            if (Input.GetMouseButton(0))
            {
                if (timeToAuto < switchTime)
                {
                    stateMachine.ChangeState(controller.autoFireState);
                }

                switchTime += Time.deltaTime;
            }
            else
            {
                stateMachine.ChangeState(controller.idleGunState);
                controller.ResetRecoil();
            }
        }

        currentTime += Time.deltaTime;
    }

    public override void Exit()
    {
        base.Exit();
        controller.SetFlash(false);
    }

    private void RayCastBullet()
    {
        ServiceLocator.Current.Get<IBulletService>().Hit(controller, trajectory);
    }
}
