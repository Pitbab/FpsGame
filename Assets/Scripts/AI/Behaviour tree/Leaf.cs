using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Leaf : Node
{
    public delegate Status Tick();

    public Leaf() { }

    public Leaf(string n, Tick pm)
    {
        name = n;
        ProcessMethod = pm;
    }
    
    public Leaf(string n, Tick pm, int order)
    {
        name = n;
        ProcessMethod = pm;
        sortOrder = order;
    }

    public Tick ProcessMethod;

    public override Status Process()
    {
        if (ProcessMethod != null)
        {
            return ProcessMethod();
        }

        return Status.FAILURE;
    }
}
