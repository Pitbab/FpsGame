using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningGunState : GunState
{
    public RunningGunState(StateMachine stateMachine, string animationBool, TempContoller controller, GunData gunData) : base(stateMachine, animationBool, controller, gunData) {}
    private bool isHolding;
    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        
        isHolding = isRunning;
        
        if (isAiming)
        {
            stateMachine.ChangeState(controller.idleGunState);
        }
        
        if (controller.mouvementController.stateMachine.currentState == controller.mouvementController.playerVaultingState)
        {
            stateMachine.ChangeState(controller.vaultingGunState);
        }

        if (isHolding) return;
        stateMachine.ChangeState(controller.idleGunState);
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
