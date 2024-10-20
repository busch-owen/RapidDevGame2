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
    
    [field:SerializeField]public bool FoundItems{ get; private set; }
    [field:SerializeField]public TextIndex TextIndex{ get; private set; }
    
    [field:SerializeField]public String FoundText{ get; private set; }
    
    [field:SerializeField]public String OpeningLine{ get; private set; }
    
    [field:SerializeField]public String NotFoundText{ get; private set; }
    
    [field:SerializeField]public List<ItemTypeSo> Items{ get; private set; } = new();

    [field: SerializeField] public List<ItemTypeSo> ItemsCollected{ get; private set; } = new();
    
    [field: SerializeField] public List<NpcTypeSo> NpcTypeSoOptions{ get; private set; } = new();
    [field: SerializeField] public List<Shelf> Shelves { get; private set; } = new ();
    [field:SerializeField]public Register[] Registers{ get; private set; }
    [field:SerializeField]public NavMeshAgent Agent { get; private set; }
    [field:SerializeField]public Transform Target { get; set; }
    
    [field:SerializeField]public ItemTypeSo ItemTypeSo { get; set; }
    [field: SerializeField] public int LastRandom { get; private set; } = 0;
    [field:SerializeField]public int RandomTarget { get; set; }
    
    [field:SerializeField] public int RandomMessage{ get; set; }

    [field: SerializeField] public int LastRandomMessage { get; private set; } = 0;
    
    [field:SerializeField]public Transform Exit { get; set; }
    
    [field:SerializeField]public NpcTypeSo NpcType { get; set; }
    
    [field:SerializeField]public int Budget { get; private set; }
    
    [field:SerializeField]public SpriteRenderer SpriteRenderer { get; private set; }
    
    [field:SerializeField]public int ShelvesBeforeLeave { get; private set; }
    
    [field:SerializeField]public float Reputation { get; set; }

    [field:SerializeField] public MoneyManager MoneyManager{ get; set; }
    
    [field:SerializeField] public int MoneySpent { get; private set; }
    
    

    
    
    // Start is called before the first frame update
    void Start()
    {
        Exit = FindFirstObjectByType<Exit>().transform;
        Agent.updateRotation = false;
        Agent.speed = NpcType.Speed;
        Budget = NpcType.Budget;
        ShelvesBeforeLeave = NpcType.ShelvesTillExit;
        SpriteRenderer.color = NpcType.Color;
        MoneyManager = FindFirstObjectByType<MoneyManager>();
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
        ChooseNpc();
        TextIndex = GetComponentInChildren<TextIndex>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        ChangeState(_npcEnterState);
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public void IntroText()
    {
        LastRandomMessage = RandomMessage;
        RandomMessage = Random.Range(0, NpcType.OpeningText.Count);

        OpeningLine = NpcType.OpeningText[RandomMessage];
        TextIndex.StartCoroutine(TextIndex.TextVisible(OpeningLine));
    }

    private void ChooseNpc()
    {
        int _randomNpc = Random.Range(0, NpcTypeSoOptions.Count);

        NpcType = NpcTypeSoOptions[_randomNpc];
    }

    public void StartText()
    {
        IntroText();
        //TextIndex.StopAllCoroutines();
        TextIndex.EnableText();
    }

    public void ChooseTarget()
    {
        LastRandom = RandomTarget;
        RandomTarget = Random.Range(0, Shelves.Count);
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

    public void ChoosePositiveDialog()
    {
        LastRandomMessage = RandomMessage;
        RandomMessage = Random.Range(0, NpcType.PositiveText.Count);

        if (LastRandomMessage != RandomMessage)
        {
            FoundText = NpcType.PositiveText[RandomMessage];
        }
        else
        {
            ChoosePositiveDialog();
        }
    }

    public void ChooseNegativeDialog()
    {
        LastRandomMessage = RandomMessage;
        RandomMessage = Random.Range(0, NpcType.NegativeText.Count);

        if (LastRandomMessage != RandomMessage)
        {
            NotFoundText = NpcType.NegativeText[RandomMessage];
        }
        else
        {
            ChooseNegativeDialog();
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
    
    
    public void ShelfCheck()
    {
        var shelf = Target.GetComponent<Shelf>();

        foreach (ItemTypeSo item in Items)
        {
            if (shelf.AssignedItem != item||Budget < shelf.AssignedItem.Cost)
            {
                ShelvesBeforeLeave--;
                return;
            }
            else
            {
                ItemsCollected.Add(item);
                MoneySpent += shelf.AssignedItem.Cost;
                FoundItems = true;
                return;
            }
        }
        Shelves.Remove(shelf);
        FoundItems = false;
    }
    public void DistanceCheck()
    {
        if (ArrivedAtTarget())
        {
            Debug.Log("arrived");
            ChangeState(_npcShelfCheckState);
        }
    }

    public void ChangeTextPositive()
    {
        ChoosePositiveDialog();
        TextIndex.StopAllCoroutines();
        TextIndex.EnableText();
        TextIndex.StartCoroutine(TextIndex.TextVisible(FoundText));
    }

    public void ChangeTextNegative()
    {
        ChooseNegativeDialog();
        TextIndex.StopAllCoroutines();
        TextIndex.EnableText();
        TextIndex.StartCoroutine(TextIndex.TextVisible(NotFoundText));
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
        var tempList = FindObjectsByType<Shelf>(FindObjectsSortMode.None);
        foreach (var shelf in tempList)
        {
            Shelves.Add(shelf);
        }

        //shelves.Add(shelf);
    }

    public void AssignRegisters()
    {
        Registers = FindObjectsByType<Register>(FindObjectsSortMode.None);
    }
}
