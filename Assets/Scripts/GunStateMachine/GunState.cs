using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunState : State
{
    protected StateMachine stateMachine;
    private string animationBool;
    protected TempContoller controller;
    protected GunState(StateMachine stateMachine, string animationBool, TempContoller controller)
    {
        this.animationBool = animationBool;
        this.stateMachine = stateMachine;
        this.controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        controller._animator.SetBool(animationBool, true);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdated()
    {
        base.FixedUpdated();
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void Exit()
    {
        base.Exit();
        controller._animator.SetBool(animationBool, false);
    }
}
