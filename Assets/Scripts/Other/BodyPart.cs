using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : Hitable
{
    [SerializeField] private float damageMultiplier;
    [SerializeField] private float partHp;
    [SerializeField] private float timeToDelete;
    private BasicEnemyController controller;
    private bool isDetached = false;


    private void Start()
    {
        //get owner hp
        controller = transform.parent.GetComponent<BasicEnemyController>();
    }

    private void Detach(Vector3 dir)
    {
        GameObject part = this.gameObject;

        part.transform.parent = null;
        isDetached = true;
        part.AddComponent<Rigidbody>().AddForce(dir * 20f, ForceMode.Impulse);
        Destroy(part, timeToDelete);
    }

    public void SetOwner(BasicEnemyController owner)
    {
        controller = owner;
    }

    public override void Hit(Vector3 dir, float damage)
    {
        if (isDetached) return;
        
        partHp -= damage * damageMultiplier;

        if (partHp <= 0)
        {
            //Detach(dir);
        }
        
        controller.TakeDamage(damage * damageMultiplier);
        
    }
}
