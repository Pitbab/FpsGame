using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandingState : PlayerGroundedState
{
    public PlayerStandingState(BasicPlayerController playerController, StateMachine stateMachine) : base(playerController, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        jump = false;
        crouch = false;
        speed = playerController.walkingSpeed;
    }

    public override void Update()
    {
        base.Update();
        if (jump)
        {
            stateMachine.ChangeState(playerController.playerJumpingState);
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
