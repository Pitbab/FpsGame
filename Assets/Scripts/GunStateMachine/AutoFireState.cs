using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFireState : GunState
{
    public AutoFireState(StateMachine stateMachine, string animationBool, TempContoller controller) : base(stateMachine, animationBool, controller) {}

    private bool isHolding;
    private float lastShot;
    private Ray trajectory;
    public override void Enter()
    {
        base.Enter();
        controller.SetFlash(true);
        controller.PlayLoop();
        lastShot = 0f;

    }

    public override void Update()
    {
        base.Update();
        HandleInput();

        if (lastShot <= 0)
        {
            RayCastBullet();
            lastShot = controller.rateOfFire;
            controller.Recoil();
        }

        lastShot -= Time.deltaTime;
        
        if (isHolding) return;
        controller.ResetRecoil();
        stateMachine.ChangeState(controller.idleGunState);
    }

    public override void HandleInput()
    {
        base.HandleInput();
        isHolding = Input.GetMouseButton(0);
    }

    public override void Exit()
    {
        base.Exit();
        controller.SetFlash(false);
        controller._audioSource.Stop();
        controller._audioSource.loop = false;
        controller.PlaySingleShot();

    }
    
    private void RayCastBullet()
    {
        trajectory.direction = controller.cam.transform.forward;
        trajectory.origin = controller.cam.transform.position;

        RaycastHit[] hits = Physics.RaycastAll(trajectory, 1000f, ~controller.ignore);

        if (hits.Length > 0)
        {
            RaycastHit firstHit = hits[hits.Length - 1];
            float smallestDist = 1000f;

            foreach (var hit in hits)
            {
                float dist = Vector3.Distance(hit.point, controller.transform.position);
            
                if (dist < smallestDist)
                {
                    smallestDist = dist;
                    firstHit = hit;
                }
            }

            controller.SpawnEffect(firstHit.point, firstHit.normal);
            
        }
        
    }
}
