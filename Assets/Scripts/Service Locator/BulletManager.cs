using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletManager : IBulletService
{

    private GameObject targetHit;
    private Hitable hitable;
    private GameObject bulletHoleSfx, bulletImpactSfx;
    public void InitService()
    {
        Debug.Log("Bullet manager is now available ");
    }

    public void Hit(TempContoller shooter, Ray ray)
    {
        ray.direction = shooter.cam.transform.forward;
        ray.origin = shooter.cam.transform.position;

        RaycastHit[] hits = Physics.RaycastAll(ray, 1000f, ~shooter.ignore);

        if (hits.Length > 0)
        {
            RaycastHit firstHit = hits[hits.Length - 1];
            float smallestDist = 1000f;

            foreach (var hit in hits)
            {
                float dist = Vector3.Distance(hit.point, shooter.transform.position);

                if (dist < smallestDist)
                {
                    smallestDist = dist;
                    firstHit = hit;
                }
            }

            targetHit = firstHit.collider.gameObject;
            hitable = targetHit.GetComponent<Hitable>();
            shooter.SpawnEffect(firstHit.point, firstHit.normal);
            
            if (hitable != null)
            {
                hitable.Hit();
            }

        }

    }
}
