using System;
using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class NPCAI : MonoBehaviour
{
    public NavMeshAgent Agent;

    public Transform target;

    public Transform[] targets;

    public List<ItemTypeSo> items;
    
    [field: SerializeField] public bool hasFoundItems  { get; private set; }
    private int randomTarget;
    private int lastRandom;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        if (target == null)
        {
            Agent.updateRotation = false;
            ChooseTarget();
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
                    SetTarget(shelf.transform);// set to check out when all items found.
                    //set has found items to true when all found
                    break;
                }
            }
        }
    }

    public void ChooseTarget()
    {
        if (!hasFoundItems)
        {
            lastRandom = randomTarget;
            randomTarget = Random.Range(0, targets.Length);

            if (lastRandom != randomTarget)
            {
                target = targets[randomTarget];
                Agent.SetDestination(target.position);
            }
            else
            {
                ChooseTarget();
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
