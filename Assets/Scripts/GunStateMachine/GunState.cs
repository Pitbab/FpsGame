using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunState : State
{
    protected StateMachine stateMachine;
    private string animationBool;
    protected TempContoller controller;
    protected GunData gunData;
    protected bool isAiming;
    private bool oldInput;
    protected bool isShooting;
    protected bool isRunning;
    protected bool inMenu = false;
    private float aimFov;

    private Coroutine currentRoutine;

    public Action<bool> OnAimStateChanged;
    public Action<bool> OnMenuStateChanged;
    
    protected GunState(StateMachine stateMachine, string animationBool, TempContoller controller, GunData gunData)
    {
        this.animationBool = animationBool;
        this.stateMachine = stateMachine;
        this.controller = controller;
        this.gunData = gunData;
    }

    public override void Enter()
    {
        base.Enter();
        controller._animator.SetBool(animationBool, true);

        aimFov = gunData.AimingFov;
        
        
        //Subscribe to action here
        OnAimStateChanged += controller.playerUI.SwitchCrossHairMode;
        OnAimStateChanged += controller.SetSway;
        OnMenuStateChanged += controller.mouseLook.SetStateMode;
        OnMenuStateChanged += controller.playerUI.SwitchMenuState;
        OnMenuStateChanged += controller.mouvementController.SwitchMenuState;
    }

    public override void Update()
    {
        base.Update();
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inMenu = !inMenu;
            OnMenuStateChanged?.Invoke(inMenu);
        }
        
        if(inMenu) return;
        isShooting = Input.GetMouseButton(0);
        
        oldInput = isAiming;
        isAiming = Input.GetMouseButton(1);
        
        if (!isAiming)
        {
            isRunning = Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0;
        }
        
        //optimising update with return condition to avoid setting aim state every frame
        if (oldInput == isAiming) return;
        
        
        if (isAiming)
        {
            isRunning = false;
            //Vector3 workspace;
            //workspace = gunData.AimingPos;
            //workspace.z *= (((controller.mouseLook.baseFov / 60f) + 1f) * 0.2f);
           // controller.transform.localPosition = workspace;
            //controller.SetFov(aimFov);
            if (currentRoutine != null)
            {
                controller.StopCoroutine(currentRoutine);
            }
            currentRoutine = controller.StartCoroutine(controller.AimLerp());
            OnAimStateChanged?.Invoke(false);
        }
        else
        {
            if (currentRoutine != null)
            {
                controller.StopCoroutine(currentRoutine);
            }

            currentRoutine = controller.StartCoroutine(controller.DeAimLerp());
            //controller.transform.localPosition = Vector3.zero;
            //controller.SetFov(controller.mouseLook.baseFov);
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
        OnAimStateChanged -= controller.SetSway;
        OnMenuStateChanged -= controller.mouseLook.SetStateMode;
        OnMenuStateChanged -= controller.playerUI.SwitchMenuState;
        OnMenuStateChanged -= controller.mouvementController.SwitchMenuState;
    }
}
