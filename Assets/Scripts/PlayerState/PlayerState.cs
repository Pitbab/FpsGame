using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : State
{

    protected BasicPlayerController playerController;
    protected StateMachine stateMachine;
    protected PlayerData playerData;
    public float baseSpeed;
    protected bool jump;
    protected float startTime;

    protected PlayerState(BasicPlayerController playerController, StateMachine stateMachine, PlayerData playerData)
    {
        this.playerController = playerController;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
    }
    public override void Enter()
    {
        base.Enter();
        startTime = Time.time;
    }

    public override void Update()
    {
        base.Update();
        jump = Input.GetKeyDown(KeyCode.Space);
    }

    public override void FixedUpdated()
    {
        base.FixedUpdated();
    }

    public override void Exit()
    {
        base.Exit();
        playerController.lastState = this;
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }
}
