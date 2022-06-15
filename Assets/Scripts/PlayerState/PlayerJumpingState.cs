using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : InAirState
{
    public PlayerJumpingState(BasicPlayerController playerController, StateMachine stateMachine) : base(playerController, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerController.Jump();
        speed = playerController.walkingSpeed;
    }

    public override void Update()
    {
        base.Update();

        if (playerController.controller.velocity.y < 0)
        {
            stateMachine.ChangeState(playerController.playerFallingState);
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
