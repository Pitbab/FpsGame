using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleReloadState : GunState
{
    private float totalTime;
    private float currentTime;
    public SimpleReloadState(StateMachine stateMachine, string animationBool, TempContoller controller, GunData gunData) : base(stateMachine, animationBool, controller, gunData) {}
    public override void Enter()
    {
        base.Enter();
        currentTime = 0f;
        totalTime = 1f;
        controller.Reload();
    }

    public override void Update()
    {
        base.Update();

        if (totalTime < currentTime)
        {
            stateMachine.ChangeState(controller.idleGunState);
        }

        currentTime += Time.deltaTime;


    }

    public override void Exit()
    {
        base.Exit();
    }
}
