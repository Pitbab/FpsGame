using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//super state that contains all state that are on ground
public class PlayerGroundedState : PlayerState
{
    protected bool crouch;
    protected bool moving;
    protected bool running;
    protected bool shooting;
    protected float horAxis;
    protected float verAxis;
    
    
    protected PlayerGroundedState(BasicPlayerController playerController, StateMachine stateMachine, PlayerData playerData) : base(playerController, stateMachine, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerController.playerFallingState.RestartTimer();
    }

    public override void Update()
    {
        base.Update();

        if (!playerController.CheckGround())
        {
            stateMachine.ChangeState(playerController.playerFallingState);
            playerController.playerFallingState.StartCoyoteTime();
        }
        
        crouch = Input.GetKeyDown(KeyCode.LeftControl);
        running = Input.GetKey(KeyCode.LeftShift);
        moving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) ||
                 Input.GetKey(KeyCode.S);
        shooting = Input.GetKeyDown(KeyCode.Mouse0);
        horAxis = Input.GetAxis("Horizontal");
        verAxis = Input.GetAxis("Vertical");
        
        playerController.GroundedVelocity();
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
