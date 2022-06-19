using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandingState : PlayerGroundedState
{
    public PlayerStandingState(BasicPlayerController playerController, StateMachine stateMachine, PlayerData playerData) : base(playerController, stateMachine, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        jump = false;
        crouch = false;
        baseSpeed = playerData.WalkSpeed;
    }

    public override void Update()
    {
        base.Update();
        
        playerController.Move(horAxis, verAxis, baseSpeed);
        
        if (jump)
        {
            stateMachine.ChangeState(playerController.playerJumpingState);
        }

        if (running  && verAxis > 0f)
        {
            stateMachine.ChangeState(playerController.playerRunningState);
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
