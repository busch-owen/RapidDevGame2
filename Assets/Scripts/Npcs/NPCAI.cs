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

    public List<Shelf> shelves = new List<Shelf>();

    public Shelf[] Shelves;

    public List<ItemTypeSo> items;

    private int itemsCollected;

    [SerializeField]private TextIndex _textIndex;

    public Register[] Registers;

    public Transform exit;

    private bool checkOutReached;

    public string foundText;

    public string  notFoundText;

    private bool isExiting;

    [SerializeField] private int timeToExit;

    [SerializeField] private int timeToExitFromCheckOut;
    
    [field: SerializeField] public bool hasFoundItems  { get; private set; }
    private int randomTarget;
    private int lastRandom;

    private ObjectPlacer _objectPlacer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        AssignShelves();
        AssignRegisters();
        target = FindFirstObjectByType<Shelf>().transform;
        exit = FindFirstObjectByType<Exit>().transform;
        Invoke("Exit",timeToExit);
        Agent = GetComponent<NavMeshAgent>();
        _textIndex = GetComponentInChildren<TextIndex>();
        Agent.updateRotation = false;
        Agent.SetDestination(target.position);

        if (target == null)
        {
            ChooseTarget();
        }
    }

    private void Exit()
    {
        isExiting = true;
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
                var targettoset = Random.Range(0, Registers.Length);
                SetTarget(Registers[targettoset].transform);
            }
        }

        if (other.GetComponent<Register>() && hasFoundItems)
        {
            checkOutReached = true;
            Invoke("Exit", timeToExitFromCheckOut);
        }

        if (other.GetComponent<Exit>() && isExiting)
        {
            Destroy(this.gameObject);
        }
    }

    public void ChooseTarget()
    {
        if (!hasFoundItems)
        {
            lastRandom = randomTarget;
            randomTarget = Random.Range(0, Shelves.Length);

            if (lastRandom != randomTarget)
            {
                target = Shelves[randomTarget].transform;
                Agent.SetDestination(target.position);
            }
            else
            {
                ChooseTarget();
            }
        }
    }

    public void AssignShelves()
    {
        Shelves = Resources.FindObjectsOfTypeAll<Shelf>();
        
        //shelves.Add(shelf);
    }

    public void AssignRegisters()
    {
        Registers = Resources.FindObjectsOfTypeAll<Register>();
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
