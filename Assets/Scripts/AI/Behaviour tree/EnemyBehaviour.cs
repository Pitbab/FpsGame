using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : BTagent
{
    [SerializeField] private List<Transform> waypoints = new List<Transform>();

    private int currentIndex = 0;
    private float waitingTime = 2f;
    private float timer = 0f; 

    public override void Start()
    {
        base.Start();
        Sequence walk = new Sequence("walk");
        Leaf walkToOne = new Leaf("walk to one", GoToOne);
        Leaf walkToTwo = new Leaf("walk to two", GoToTwo);
        Leaf wait = new Leaf("wait", Wait);

        
        walk.AddChild(walkToOne);
        walk.AddChild(wait);
        walk.AddChild(walkToTwo);
        walk.AddChild(wait);
        
        tree.AddChild(walk);

    }

    private Node.Status Wait()
    {
        if (state == ActionState.IDLE)
        {
            StartCoroutine(Waiting());
            state = ActionState.WORKING;
        }
        else if(timer >= waitingTime)
        {
            state = ActionState.IDLE;
            return Node.Status.SUCCESS;
        }

        return Node.Status.RUNNING;
    }

    private IEnumerator Waiting()
    {
        timer = 0f;
        
        while (timer < waitingTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
    }
    
    private Node.Status GoToOne()
    {
        return GoToLocation(waypoints[0].position);
    }

    private Node.Status GoToTwo()
    {
        return GoToLocation(waypoints[1].position);
    }

    private Node.Status GoToThree()
    {
        return GoToLocation(waypoints[2].position);
    }
    
    private Node.Status GoToFour()
    {
        return GoToLocation(waypoints[3].position);
    }
    
    private Node.Status WalkToWaypoint()
    {
        return GoToLocation(waypoints[currentIndex].position);
    }

    private Node.Status GoToLocation(Vector3 destination)
    {
        float distanceToTarget = Vector3.Distance(destination, this.transform.position);
        
        if (state == ActionState.IDLE)
        {
            agent.SetDestination(destination);
            state = ActionState.WORKING;
        }
        else if(Vector3.Distance(agent.pathEndPosition, destination) >= 2)
        {
            return Node.Status.FAILURE;
        }
        else if(distanceToTarget < 2)
        {
             state = ActionState.IDLE;
             return Node.Status.SUCCESS;
        }

        return Node.Status.RUNNING;
    }
    
}
