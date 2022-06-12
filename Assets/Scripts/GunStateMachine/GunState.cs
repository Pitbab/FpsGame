using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunState : State
{
    protected StateMachine stateMachine;
    private string animationBool;
    protected TempContoller controller;
    protected bool isAiming;
    private bool oldInput;
    protected bool isShooting;
    protected bool isRunning;
    protected bool inMenu = false;

    public Action<bool> OnAimStateChanged;
    public Action<bool> OnMenuStateChanged;
    
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
        
        
        //Subscribe to action here
        OnAimStateChanged += controller.playerUI.SwitchCrossHairMode;
        OnMenuStateChanged += controller.mouseLook.SetStateMode;
        OnMenuStateChanged += controller.playerUI.SwitchMenuState;
        OnMenuStateChanged += controller.mouvementController.SwitchMenuState;
    }

    public override void Update()
    {
        base.Update();

        
        //need to gain access to the basic movement controller and the camera controller
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inMenu = !inMenu;
            OnMenuStateChanged?.Invoke(inMenu);
        }
        
        if(inMenu) return;
        
        isRunning = Input.GetKey(KeyCode.LeftShift);
        isShooting = Input.GetMouseButton(0);
        
        oldInput = isAiming;
        isAiming = Input.GetMouseButton(1);
        
        //optimising update with return condition to avoid setting aim state every frame
        if (oldInput == isAiming) return;
        
        
        if (isAiming)
        {
            controller.SetAimPos();
            controller.SetSway(false);
            OnAimStateChanged?.Invoke(false);
        }
        else
        {
            controller.transform.localPosition = Vector3.zero;
            controller.SetSway(true);
            OnAimStateChanged?.Invoke(true);
        }


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
        
        //Unsubscribe to action here
        OnAimStateChanged -= controller.playerUI.SwitchCrossHairMode;
    }
}
