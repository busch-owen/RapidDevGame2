using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.StateMachine;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public enum NpcStateName
{
    Enter,Wander,CheckShelf,CorrectItem,IncorrectItem,Dialog,Checkout,Exit
}
public class NpcStateMachine : BaseStateMachine
{

    private NpcCheckoutState _npcCheckoutState;
    private NpcDialogState _npcDialogState;
    private NpcWanderState _npcWanderState;
    private NpcExitState _npcExitState;
    private NpcShelfCheckState _npcShelfCheckState;
    private NPCBaseState _baseState;
    private NpcEnterState _npcEnterState;
    
    [field:SerializeField]public TextIndex TextIndex{ get; set; }
    
    [field:SerializeField]public String FoundText{ get; set; }
    
    [field:SerializeField]public String NotFoundText{ get; set; }
    
    [field:SerializeField]public List<ItemTypeSo> Items{ get; private set; } = new();

    [field: SerializeField] public List<ItemTypeSo> ItemsCollected{ get; private set; } = new(); 
    [field:SerializeField]public Shelf[] Shelves{ get; private set; }
    [field:SerializeField]public Register[] Registers{ get; private set; }
    [field:SerializeField]public NavMeshAgent Agent { get; private set; }
    [field:SerializeField]public Transform Target { get; set; }
    [field:SerializeField]public ItemTypeSo ItemTypeSo { get; set; }
    private int _lastRandom;
    private int _randomTarget;

    
    
    // Start is called before the first frame update
    void Start()
    {
        TextIndex = GetComponentInChildren<TextIndex>();
    }

    private void Awake()
    {
        _npcCheckoutState = new NpcCheckoutState(this);
        _npcDialogState = new NpcDialogState(this);
        _npcWanderState = new NpcWanderState(this);
        _npcExitState = new NpcExitState(this);
        _npcShelfCheckState = new NpcShelfCheckState(this);
        _npcEnterState = new NpcEnterState(this);
        AssignShelves();
        AssignRegisters();

        ChangeState(_npcEnterState);
    }

    public void ChooseTarget()
    {
        _lastRandom = _randomTarget;
        _randomTarget = Random.Range(0, Shelves.Length);
        if (_lastRandom != _randomTarget)
        {
            Target = Shelves[_randomTarget].transform;
            Agent.SetDestination(Target.position);
        }
        else
        {
            ChooseTarget();
        }
        
    }
    
    private bool ArrivedAtTarget()
    {
        if (Vector2.Distance(transform.position, Agent.destination) <= 0.25f)
        {
            return true;
        }

        return false;
    }

    public void DistanceCheck()
    {
        if (ArrivedAtTarget())
        {
            Debug.Log("arrived");
            ChangeState(_npcShelfCheckState);
        }
        else
        {
            Debug.Log("notArrived");
        }
    }

    // Update is called once per frame

    public void ChangeState(NpcStateName stateName)
    {
        switch (stateName)
        {
            case NpcStateName.Checkout:
                base.ChangeState(_npcCheckoutState);
                break;
            case NpcStateName.Dialog:
                base.ChangeState(_npcDialogState);
                break;
            case NpcStateName.Enter:
                base.ChangeState(_npcEnterState);
                break;
            case NpcStateName.Exit:
                base.ChangeState(_npcExitState);
                break;
            case NpcStateName.CheckShelf:
                base.ChangeState(_npcShelfCheckState);
                Debug.Log("ShelfCheck");
                break;
            case NpcStateName.Wander:
                base.ChangeState(_npcWanderState);
                Debug.Log("wander");
                break;
                
                
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
