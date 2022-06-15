using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected float speed;
    protected bool jump;
    protected bool crouch;
    protected bool moving;
    protected bool running;
    protected bool shooting;
    protected float horAxis;
    protected float verAxis;
    
    
    protected PlayerGroundedState(BasicPlayerController playerController, StateMachine stateMachine) : base(playerController, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (!playerController.CheckGround())
        {
            stateMachine.ChangeState(playerController.playerFallingState);
        }
        
        crouch = Input.GetKeyDown(KeyCode.LeftControl);
        jump = Input.GetKeyDown(KeyCode.Space);
        running = Input.GetKey(KeyCode.LeftShift);
        moving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) ||
                 Input.GetKey(KeyCode.S);
        shooting = Input.GetKeyDown(KeyCode.Mouse0);
        horAxis = Input.GetAxis("Horizontal");
        verAxis = Input.GetAxis("Vertical");
        
        playerController.Move(horAxis, verAxis, speed);
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