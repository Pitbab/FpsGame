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
    protected bool coyoteTime;
    private float timer;

    public override void Enter()
    {
        base.Enter();
        //baseSpeed = playerController.lastState.baseSpeed;
    }

    public override void Update()
    {
        base.Update();
        
        UpdateCoyoteTime();
        
        horAxis = Input.GetAxis("Horizontal");
        verAxis = Input.GetAxis("Vertical");
        canVault = playerController.CheckVault();

        playerController.Move(horAxis, verAxis, playerController.currentSpeed);
        playerController.FallingVelocity();

        if (canVault && jumpBuffer)
        {
            stateMachine.ChangeState(playerController.playerVaultingState);
        }
        
        if (playerController.controller.velocity.y < 0.01f)
        {
            stateMachine.ChangeState(playerController.playerFallingState);
        }

        if (jump && coyoteTime)
        {   
            stateMachine.ChangeState(playerController.playerJumpingState);
        }
    }
    
    private void UpdateCoyoteTime()
    {
        if (coyoteTime)
        {
            timer += Time.deltaTime;
        }
        
        if (coyoteTime && timer > playerData.CoyoteTime)
        {
            coyoteTime = false;
        }
    }
    
    public void StartCoyoteTime() => coyoteTime = true;
    public void RestartTimer() => timer = 0f;

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
