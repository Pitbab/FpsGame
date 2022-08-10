using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : Hitable
{
    [SerializeField] private int points;
    [SerializeField] private AudioClip hitMarker;

    
    public override void Hit(Vector3 dir, float damage)
    {
        base.Hit(dir, damage);
        Destroy(transform.parent.gameObject);
        ServiceLocator.Current.Get<ISoundService>().PlaySound(hitMarker, transform.position, 1f);
    }

}
