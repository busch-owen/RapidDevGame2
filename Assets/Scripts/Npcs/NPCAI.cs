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

    private int itemsCollected;

    [SerializeField]private TextIndex _textIndex;

    public Transform checkOut;

    public Transform exit;

    private bool checkOutReached;

    public string foundText;

    public string  notFoundText;

    [SerializeField] private int timeToExit;

    [SerializeField] private int timeToExitFromCheckOut;
    
    [field: SerializeField] public bool hasFoundItems  { get; private set; }
    private int randomTarget;
    private int lastRandom;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Exit",timeToExit);
        checkOut = FindFirstObjectByType<Register>().transform;
        Agent = GetComponent<NavMeshAgent>();
        _textIndex = GetComponentInChildren<TextIndex>();
        if (target == null)
        {
            Agent.updateRotation = false;
            ChooseTarget();
        }
    }

    private void Exit()
    {
        SetTarget(exit);
    }

    private void OnTriggerEnter2D(Collider2D other)// figure out state machines
    {
        var shelf = other.GetComponent<Shelf>();
        if (shelf)
        {
            foreach (var itemType in items)
            {
                if (itemType == shelf.AssignedItem)// need to figure out removing from list so that it doesn't go for the same again.
                {
                    StartCoroutine(_textIndex.TextVisible(foundText));
                    _textIndex.EnableText();
                    itemsCollected++;
                    SetTarget(shelf.transform);// set to check out when all items found.
                    //set has found items to true when all found
                    break;
                }
                else
                {
                    StartCoroutine(_textIndex.TextVisible(notFoundText));
                    _textIndex.EnableText();
                }
            }

            if (itemsCollected >= items.Count )
            {
                hasFoundItems = true;
                SetTarget(checkOut);
            }
        }

        if (other.GetComponent<Register>())
        {
            checkOutReached = true;
            Invoke("Exit", timeToExitFromCheckOut);
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
