using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandingState : PlayerGroundedState
{

    private float StepTimer = 0.4f;
    private float currentTime = 0f;
    public PlayerStandingState(BasicPlayerController playerController, StateMachine stateMachine, PlayerData playerData) : base(playerController, stateMachine, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        jump = false;
        crouch = false;
        playerController.currentSpeed = playerData.WalkSpeed;
    }

    public override void Update()
    {
        base.Update();
        
        playerController.Move(horAxis, verAxis, playerController.currentSpeed);

        if (horAxis != 0 || verAxis != 0)
        {
            currentTime += Time.deltaTime;
            if (currentTime > StepTimer)
            {
                playerController.audioSource.PlayOneShot(playerData.walkOnConcrete[Random.Range(0, playerData.walkOnConcrete.Capacity)]);
                currentTime = 0;
            }
        }
        
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
