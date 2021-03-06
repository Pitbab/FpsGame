using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : InAirState
{
    public PlayerFallingState(BasicPlayerController playerController, StateMachine stateMachine, PlayerData playerData) : base(playerController, stateMachine, playerData)
    {
    }
    
    //private bool coyoteTime;

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        //if (jump && coyoteTime)
        //{
            //stateMachine.ChangeState(playerController.playerJumpingState);
        //}
        
        if (playerController.CheckGround())
        {
            stateMachine.ChangeState(playerController.playerStandingState);
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
