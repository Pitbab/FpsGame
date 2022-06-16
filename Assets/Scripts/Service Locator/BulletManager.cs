using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : IBulletService
{
    public void InitService()
    {
        Debug.Log("test");
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

            shooter.SpawnEffect(firstHit.point, firstHit.normal);
            
            Debug.Log(firstHit.collider.gameObject.name);

            Hitable t = firstHit.collider.GetComponent<Hitable>();
            if (t != null)
            {
                t.Hit();
            }

        }

    }
}
