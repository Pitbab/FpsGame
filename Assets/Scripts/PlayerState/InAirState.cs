using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// super state that contains all states that are airborn
public class InAirState : PlayerState
{
    protected InAirState(BasicPlayerController playerController, StateMachine stateMachine, PlayerData playerData) : base(playerController, stateMachine, playerData)
    {
    }
    
    protected float horAxis;
    protected float verAxis;
    protected bool canVault;

    public override void Enter()
    {
        base.Enter();
        baseSpeed = playerController.lastState.baseSpeed;
    }

    public override void Update()
    {
        base.Update();
        horAxis = Input.GetAxis("Horizontal");
        verAxis = Input.GetAxis("Vertical");
        canVault = playerController.CheckVault();

        playerController.Move(horAxis, verAxis, baseSpeed);
        playerController.FallingVelocity();

        if (canVault && jump)
        {
            stateMachine.ChangeState(playerController.playerVaultingState);
        }
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
