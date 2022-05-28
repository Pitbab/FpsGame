using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleReloadState : GunState
{
    private float totalTime;
    private float currentTime;
    public SimpleReloadState(StateMachine stateMachine, string animationBool, TempContoller controller) : base(stateMachine, animationBool, controller) {}
    public override void Enter()
    {
        base.Enter();
        currentTime = 0f;
        totalTime = 0.5f;
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
