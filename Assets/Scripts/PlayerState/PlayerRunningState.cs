using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : PlayerGroundedState
{

    private Coroutine SpeedUpRoutine;
    public PlayerRunningState(BasicPlayerController playerController, StateMachine stateMachine, PlayerData playerData) : base(playerController, stateMachine, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //baseSpeed = playerData.RunSpeed;

        if (SpeedUpRoutine != null)
        {
            playerController.StopCoroutine(SpeedUpRoutine);
        }
        
        SpeedUpRoutine = playerController.StartCoroutine(SpeedingUp());

    }

    public override void Update()
    {
        base.Update();

        //limit the player to turn when running
        horAxis /= 2;
        
        playerController.Move(horAxis, verAxis, playerController.currentSpeed);
        
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

        if (SpeedUpRoutine != null)
        {
            playerController.StopCoroutine(SpeedUpRoutine);
        }

    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    private IEnumerator SpeedingUp()
    {
        while (playerController.currentSpeed < playerData.RunSpeed)
        {
            playerController.currentSpeed += Time.deltaTime * 6f;
            //Debug.Log(playerController.currentSpeed);
            yield return null;
        }
    }
}
