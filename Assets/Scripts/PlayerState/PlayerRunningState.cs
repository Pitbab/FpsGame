using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : PlayerGroundedState
{
    public PlayerRunningState(BasicPlayerController playerController, StateMachine stateMachine, PlayerData playerData) : base(playerController, stateMachine, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        baseSpeed = playerData.RunSpeed;
        
    }

    public override void Update()
    {
        base.Update();

        //limit the player to turn when running
        horAxis /= 2;
        
        playerController.Move(horAxis, verAxis, baseSpeed);
        
        if (!running)
        {
            stateMachine.ChangeState(playerController.playerStandingState);
        }

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
