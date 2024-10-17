using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.StateMachine;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public enum NpcStateName
{
    Enter,Wander,CheckShelf,CorrectItem,IncorrectItem,PositiveDialog,Checkout,Exit,NegativeDialog
}
public class NpcStateMachine : BaseStateMachine
{

    private NpcCheckoutState _npcCheckoutState;
    private NpcPositiveDialogState _npcPositiveDialogState;
    private NpcWanderState _npcWanderState;
    private NpcExitState _npcExitState;
    private NpcShelfCheckState _npcShelfCheckState;
    private NPCBaseState _baseState;
    private NpcEnterState _npcEnterState;
    private NpcNegativeDialogState _npcNegativeDialogState;
    
    
    [field:SerializeField]public string Current{ get; private set; }
    [field:SerializeField]public TextIndex TextIndex{ get; private set; }
    
    [field:SerializeField]public String FoundText{ get; private set; }
    
    [field:SerializeField]public String NotFoundText{ get; private set; }
    
    [field:SerializeField]public List<ItemTypeSo> Items{ get; private set; } = new();

    [field: SerializeField] public List<ItemTypeSo> ItemsCollected{ get; private set; } = new(); 
    [field:SerializeField]public Shelf[] Shelves{ get; private set; }
    [field:SerializeField]public Register[] Registers{ get; private set; }
    [field:SerializeField]public NavMeshAgent Agent { get; private set; }
    [field:SerializeField]public Transform Target { get; set; }
    [field:SerializeField]public ItemTypeSo ItemTypeSo { get; set; }
    [field:SerializeField]public int LastRandom { get; set; }
    [field:SerializeField]public int RandomTarget { get; set; }
    
    [field:SerializeField]public Transform Exit { get; set; }
    
    

    
    
    // Start is called before the first frame update
    void Start()
    {
        TextIndex = GetComponentInChildren<TextIndex>();
        Exit = FindFirstObjectByType<Exit>().transform;
    }

    private void Awake()
    {
        _npcCheckoutState = new NpcCheckoutState(this);
        _npcPositiveDialogState = new NpcPositiveDialogState(this);
        _npcWanderState = new NpcWanderState(this);
        _npcExitState = new NpcExitState(this);
        _npcShelfCheckState = new NpcShelfCheckState(this);
        _npcEnterState = new NpcEnterState(this);
        _npcNegativeDialogState = new NpcNegativeDialogState(this);
        AssignShelves();
        AssignRegisters();

        ChangeState(_npcEnterState);
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public void ChooseTarget()
    {
        LastRandom = RandomTarget;
        RandomTarget = Random.Range(0, Shelves.Length);
        if (LastRandom != RandomTarget)
        {
            Target = Shelves[RandomTarget].transform;
            Agent.SetDestination(Target.position);
        }
        else
        {
            ChooseTarget();
        }
        
    }
    
    public bool ArrivedAtTarget()
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
    }


    public void CheckItems()
    {
        if (ItemsCollected.Count >= Items.Count)
        {
            ChangeState(_npcCheckoutState);
            Debug.Log("Checkout");
        }
    }

    // Update is called once per frame

    public void ChangeState(NpcStateName stateName)
    {
        switch (stateName)
        {
            case NpcStateName.Checkout:
                base.ChangeState(_npcCheckoutState);
                Current = _npcCheckoutState.ToString();
                break;
            case NpcStateName.PositiveDialog:
                base.ChangeState(_npcPositiveDialogState);
                Current = _npcPositiveDialogState.ToString();
                break;
            case NpcStateName.Enter:
                base.ChangeState(_npcEnterState);
                Current = _npcEnterState.ToString();
                break;
            case NpcStateName.Exit:
                base.ChangeState(_npcExitState);
                Current = _npcExitState.ToString();
                break;
            case NpcStateName.CheckShelf:
                base.ChangeState(_npcShelfCheckState);
                Current = _npcShelfCheckState.ToString();
                break;
            case NpcStateName.Wander:
                base.ChangeState(_npcWanderState);
                Current = _npcWanderState.ToString();
                break;
            case NpcStateName.NegativeDialog:
                base.ChangeState(_npcNegativeDialogState);
                Current = _npcNegativeDialogState.ToString();
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
