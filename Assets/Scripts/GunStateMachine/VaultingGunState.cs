using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultingGunState : GunState
{

    public VaultingGunState(StateMachine stateMachine, string animationBool, TempContoller controller, GunData gunData) : base(stateMachine, animationBool, controller, gunData)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (controller.mouvementController.stateMachine.currentState == controller.mouvementController.playerVaultingState)
        {
            return;
        }
        
        stateMachine.ChangeState(controller.idleGunState);
    }

    public override void FixedUpdated()
    {
        base.FixedUpdated();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }
}
