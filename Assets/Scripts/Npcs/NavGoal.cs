using System;
using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;


public class NavGoal : MonoBehaviour
{
    public NavMeshAgent Agent;

    public Transform target;

    public List<ItemTypeSo> items;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        if (target == null)
        {
            
        }
        else
        {
            Agent.updateRotation = false;
            Agent.SetDestination(target.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)// figure out state machines
    {
        var shelf = other.GetComponent<Shelf>();
        if (shelf)
        {
            foreach (var itemType in items)
            {
                if (itemType == shelf.AssignedItem)
                {
                    SetTarget(shelf.transform);
                    break;
                }
            }
        }
    }

    public void SetTarget(Transform Target)
    {
        Agent.SetDestination(Target.position);
    }
}

public enum NpcType
{
    Smelly,Karen,HighRoller,Regular,Child
}
