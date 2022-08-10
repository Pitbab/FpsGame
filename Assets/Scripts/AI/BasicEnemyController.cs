using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyController : MonoBehaviour
{
    private float hp = 200f;
    private List<Rigidbody> bodyPartsRb = new List<Rigidbody>();
    private NavMeshAgent agent;
    private Animator animator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
        bodyPartsRb = rbs.ToList();
        DisableRagdoll();

        //agent.SetDestination(new Vector3(32, 0, 0));
        
    }

    private void Update()
    {
    }

    public void TakeDamage(float amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            EnableRagdoll();
        }
            
    }

    private void DisableRagdoll()
    {
        foreach (var part in bodyPartsRb)
        {
            //part.detectCollisions = false;
            part.isKinematic = true;
            part.GetComponent<BodyPart>().SetOwner(this);
        }
    }

    private void EnableRagdoll()
    {
        animator.enabled = false;
        
        foreach (var part in bodyPartsRb)
        {
            //part.detectCollisions = true;
            part.isKinematic = false;
        }
    }
}
