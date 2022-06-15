using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : PlayerState
{
    protected InAirState(BasicPlayerController playerController, StateMachine stateMachine) : base(playerController, stateMachine)
    {
    }
    
    protected float speed;
    protected float horAxis;
    protected float verAxis;  

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        horAxis = Input.GetAxis("Horizontal");
        verAxis = Input.GetAxis("Vertical");
        
        playerController.Move(horAxis, verAxis, speed);
        playerController.FallingVelocity();
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
