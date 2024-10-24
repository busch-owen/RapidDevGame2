using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.StateMachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public enum NpcStateName
{
    Enter,Wander,CheckShelf,CorrectItem,IncorrectItem,PositiveDialog,Checkout,Exit,NegativeDialog,Spawn,Talking
}

public delegate void Selected();

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
    private NpcSpawnState _npcSpawnState;
    private NpcTalkingState _npcTalkingState;

    public event Selected SelectedEvent;
    
    
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
    
    [field:SerializeField] public int TimeForFirstWander { get; private set; }
    
    [field:SerializeField] public int TimeToLeave { get; private set; }
    
    [field:SerializeField] public bool Shoplifter { get; private set; }
    
    [field:SerializeField] public bool CanTalk { get; set; }
    
    [field:SerializeField] public SpriteRenderer Renderer { get; private set; }
    
    [field:SerializeField] public List<Image> PossibleImages{ get; private set; }

    [field:SerializeField] public List<Image> PreviousImages{ get; private set; }
    
    [field:SerializeField] public Shelf CurrentShelf{ get; set; }
    
    private bool _ranBefore;

    private bool _clicked;

    private bool _interacted;

    
    
    // Start is called before the first frame update
    void Start()
    {
        Agent.updateRotation = false;
        Agent.speed = NpcType.Speed;
        Budget = NpcType.Budget;
        ShelvesBeforeLeave = Shelves.Count;
        MoneyManager = FindFirstObjectByType<MoneyManager>();
        Shoplifter = NpcType.ShopLifter;
        Items = NpcType.Items;
        Renderer = GetComponentInChildren<SpriteRenderer>();
        Renderer.sprite = NpcType.NpcSprite;
        Renderer.color = Color.white;
        Renderer.transform.rotation = Quaternion.Euler(90,0,0);
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
        _npcSpawnState = new NpcSpawnState(this);
        _npcTalkingState = new NpcTalkingState(this);
        Exit = FindFirstObjectByType<Exit>().transform;
        AssignShelves();
        AssignRegisters();
        ChooseNpc();
        TextIndex = GetComponentInChildren<TextIndex>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        ChangeState(_npcSpawnState);
    }

    public void Leave()
    {
        Agent.destination = Exit.transform.position;
    }

    public void FirstWander()
    {
        if(_interacted)return;
        ChangeState(_npcWanderState);
    }

    public void Spawn()
    {
        Agent.SetDestination(Exit.transform.position);
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    private void ChooseNpc()// randomly select what npc it will be
    {
        int _randomNpc = Random.Range(0, NpcTypeSoOptions.Count);

        NpcType = NpcTypeSoOptions[_randomNpc];
    }

    public void ChooseTarget()// set the npc target to a random shelf
    {
        if (Shelves.Count <= 0)
        {
            ChangeState(NpcStateName.Exit);
        }
        else
        {
            RandomTarget = Random.Range(0, Shelves.Count);
            Target = Shelves[RandomTarget].transform;
            Agent.SetDestination(Target.position);
        }
    }

    public void ShowImages()
    {
        if (!_ranBefore)
        {
            foreach (var image in PossibleImages)
            {
                TextIndex.AddEmotes(image);
                PreviousImages.Add(image);
            }

            _ranBefore = true;
        }
        else
        {
            foreach (var image in PreviousImages)
            {
                TextIndex.RemoveEmotes(image);
            }
            PreviousImages.Clear();
            foreach (var image in PossibleImages)
            {
                TextIndex.AddEmotes(image);
                PreviousImages.Add(image);
            }
        }
    }

    public void ChangeToNegative()
    {
        PossibleImages = NpcType.Bad;
        ShowImages();
        TextIndex.StartCoroutine("ImageVisible");
    }

    public void ChangeToPositive()
    {
        PossibleImages = NpcType.Good;
        ShowImages();
        TextIndex.StartCoroutine("ImageVisible");
    }

    public void ShowOpening()
    {
        PossibleImages = NpcType.Opening;
        ShowImages();
        TextIndex.StartCoroutine("ImageVisible");
        
    }
    
    public bool ArrivedAtTarget()//General distance check between target and destination 
    {
        if (Vector2.Distance(transform.position, Agent.destination) <= 0.25f)
        {
            return true;
        }

        return false;
    }
    
    
    public void ShelfCheck()// check the shelf per item in the npc's list to see if the type matches and it is within their budget
    {
        var shelf = Target.GetComponent<Shelf>();
        Shelves.Remove(shelf);
        foreach (ItemTypeSo item in Items)
        {
            if (shelf.AssignedItem == null ) continue;
            if(shelf.AssignedItem.ItemCount <= 0) continue;
            if (Shoplifter)
            {
                shelf.AssignedItem.ItemCount--;
                ItemsCollected.Add(item);// add the item to the npcs list of collected items
                MoneySpent += shelf.AssignedItem.ItemType.Cost;// spend the money
                ShelvesBeforeLeave--;
                
            }
            else if (shelf.AssignedItem.ItemType != item||Budget < shelf.AssignedItem.ItemType.Cost|| shelf.AssignedItem.ItemCount <= 0)
            {
                ShelvesBeforeLeave--;// decrement the amount of shelves it takes before the npc decides to leave empty handed 
                return;
            }
            else
            {
                ItemsCollected.Add(item);// add the item to the npcs list of collected items
                shelf.AssignedItem.ItemCount--;
                MoneySpent += shelf.AssignedItem.ItemType.Cost;// spend the money
                FoundItems = true;
                return;
            }
        }
        // remove the shelf from the list so the npc does not try to go to it again
        FoundItems = false;
    }
    public void DistanceCheck()// check the distance between shelfs and the npc, if within distance switch state
    {
        if (ArrivedAtTarget())
        {
            Debug.Log("arrived");
            ChangeState(_npcShelfCheckState);
        }
    }
    // Update is called once per frame

    public void ChangeState(NpcStateName stateName)// outline the case for each state change 
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
            case NpcStateName.Spawn:
                base.ChangeState(_npcSpawnState);
                Current = _npcSpawnState.ToString();
                break;
            case NpcStateName.Talking:
                base.ChangeState(_npcTalkingState);
                Current = _npcTalkingState.ToString();
                break;
            
                
                
        }
    }
    
    public void Wander()
    {
        ChangeState(NpcStateName.Wander);
    }
    
    public void AssignShelves()// find all of the shelves in the scene and add them to a list
    {
        var tempList = FindObjectsByType<Shelf>(FindObjectsSortMode.None);
        foreach (var shelf in tempList)
        {
            Shelves.Add(shelf);
        }

        //shelves.Add(shelf);
    }

    public void AssignRegisters()// find all of the registers in the scene and add them to the array
    {
        Registers = FindObjectsByType<Register>(FindObjectsSortMode.None);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("fuck");
        if (other.GetComponentInParent<EmployeeStateMachine>())
        {
            CanTalk = true;
            var Emp = other.GetComponentInParent<EmployeeStateMachine>();
            Emp.npcStateMachine = this;
        }
    }


    public void OpenWindow()
    {
        if(!CanTalk) return;
        _interacted = true;
        foreach (var shelf in Shelves)
        {
            shelf.AssignNpc(this);
            shelf.EnableClick();
        }
        SelectedEvent?.Invoke();
    }
}
