using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : Hitable
{
    [SerializeField] private int points;

    
    public override void Hit()
    {
        base.Hit();
        Destroy(transform.parent.gameObject);
    }

}
