using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleFIreState : GunState
{
    private float totalTime;
    private float currentTime;
    private float switchTime;
    private float timeToAuto = 0.05f;
    private Ray trajectory;
    public SingleFIreState(StateMachine stateMachine, string animationBool, TempContoller controller) : base(stateMachine, animationBool, controller) {}
    public override void Enter()
    {
        base.Enter();
        currentTime = 0f;
        totalTime = 0.1f;
        switchTime = 0.05f;
        controller.SetFlash(true);
        controller.PlaySingleShot();
        RayCastBullet();
        controller.Recoil();
        
        
    }

    public override void Update()
    {
        base.Update();
        
        if (totalTime < currentTime)
        {
            if (Input.GetMouseButton(0))
            {
                if (timeToAuto < switchTime)
                {
                    stateMachine.ChangeState(controller.autoFireState);
                }

                switchTime += Time.deltaTime;
            }
            else
            {
                stateMachine.ChangeState(controller.idleGunState);
                controller.ResetRecoil();
            }
        }

        currentTime += Time.deltaTime;
    }

    public override void Exit()
    {
        base.Exit();
        controller.SetFlash(false);
    }

    private void RayCastBullet()
    {
        ServiceLocator.Current.Get<IBulletService>().Hit(controller, trajectory);
        
        /*
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
            firstHit.collider.GetComponent<Target>().Hit();
        
            
        }
        
        */
        
    }
}
