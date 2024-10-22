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

    public Shelf[] Shelves;

    public List<ItemTypeSo> items = new();

    private List<ItemTypeSo> itemsCollected = new();

    private CircleCollider2D _collider2D;

    [SerializeField]private TextIndex _textIndex;

    public Register[] Registers;

    public Transform exit;

    private bool checkOutReached;

    public string foundText;

    public string  notFoundText;

    private bool isExiting;

    [SerializeField] private int timeToExit;

    [SerializeField] private int timeToExitFromCheckOut;

    [SerializeField] private float _timeToMove = 2.0f;
    
    [field: SerializeField] public bool hasFoundItems  { get; private set; }
    private int randomTarget;
    private int lastRandom;

    private bool isTalking;

    private ObjectPlacer _objectPlacer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        AssignShelves();
        AssignRegisters();
        _collider2D = GetComponent<CircleCollider2D>();
        target = FindFirstObjectByType<Shelf>().transform;
        exit = FindFirstObjectByType<Exit>().transform;
        Invoke("Exit",timeToExit);
        Agent = GetComponent<NavMeshAgent>();
        _textIndex = GetComponentInChildren<TextIndex>();
        Agent.updateRotation = false;
        Agent.SetDestination(target.position);
        _textIndex.TextFinished += ChooseTarget;

        if (target == null)
        {
            ChooseTarget();
        }
    }

    private void Exit()
    {
        isExiting = true;
        Destroy(this.gameObject);
        
    }

    private bool ArrivedAtTarget()
    {
        if (Vector2.Distance(transform.position, Agent.destination) <= 0.25f)
        {
            return true;
        }

        return false;
    }

    private void FixedUpdate()
    {
        if (itemsCollected.Count >= items.Count && !hasFoundItems)
        {
            target = null;
            Invoke("FoundItems",2.0f);
        }
        else
        {
            ShelfCheck();
        }
    }

    private void FoundItems()
    {
        hasFoundItems = true;
        randomTarget = Random.Range(0, Registers.Length);
        Agent.SetDestination(Registers[randomTarget].transform.position);
    }
    

    private void ShelfCheck()
    {
        if (!ArrivedAtTarget() || hasFoundItems) return;
        
        var shelf = target.GetComponent<Shelf>();
        target = null;

        foreach (ItemTypeSo item in items)
        {
            if(shelf.AssignedItem.ItemType != item) continue;
            _textIndex.StopAllCoroutines();
            _textIndex.EnableText();
            StartCoroutine(_textIndex.TextVisible(foundText));
            target = null;
            Invoke(nameof(ChooseTarget), 2f);
            itemsCollected.Add(item);
            return;
        }
        _textIndex.EnableText();
        _textIndex.StopAllCoroutines();
        target = null;
        Invoke(nameof(ChooseTarget), 2f);
        StartCoroutine(_textIndex.TextVisible(notFoundText));
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
        Shelves = FindObjectsByType<Shelf>(FindObjectsSortMode.None);

        //shelves.Add(shelf);
    }

    public void AssignRegisters()
    {
        Registers = FindObjectsByType<Register>(FindObjectsSortMode.None);
    }
}
