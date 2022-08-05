using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints = new List<Transform>();
    public BehaviourTree tree;
    public NavMeshAgent agent;
    
    public enum ActionState {IDLE, WORKING};
    public ActionState state = ActionState.IDLE;
    private int currentIndex = 0;

    private Node.Status treeStatus = Node.Status.RUNNING;
    
    private void Start()
    {
        tree = new BehaviourTree();

        Sequence walk = new Sequence("walk");
        Leaf walkToOne = new Leaf("walk to one", GoToOne);
        Leaf walkToTwo = new Leaf("walk to two", GoToTwo);
        Leaf walkToThree = new Leaf("walk to three", GoToThree);
        Leaf walkToFour = new Leaf("walk to Four", GoToFour);
        Selector openDoor = new Selector("Open Door");
        
        openDoor.AddChild(walkToFour);
        openDoor.AddChild(walkToThree);
        
        walk.AddChild(openDoor);
        walk.AddChild(walkToOne);
        walk.AddChild(walkToTwo);
        
        tree.AddChild(walk);

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

    private void Update()
    {
        if (treeStatus == Node.Status.RUNNING)
        {
            treeStatus = tree.Process();
        }
    }
}
