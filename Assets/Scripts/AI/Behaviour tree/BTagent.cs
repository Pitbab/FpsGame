using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BTagent : MonoBehaviour
{
    public BehaviourTree tree;
    public NavMeshAgent agent;
    
    public enum ActionState {IDLE, WORKING};
    public ActionState state = ActionState.IDLE;
    private int currentIndex = 0;

    public Node.Status treeStatus = Node.Status.RUNNING;

    private WaitForSeconds waitForSeconds;
    
    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        tree = new BehaviourTree();
        waitForSeconds = new WaitForSeconds(Random.Range(0.1f, 1f));

        StartCoroutine(Behave());
    }

    public Node.Status GoToLocation(Vector3 destination)
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

    IEnumerator Behave()
    {
        while (true)
        {
            treeStatus = tree.Process();
            yield return waitForSeconds;
        }
    }
    
}
