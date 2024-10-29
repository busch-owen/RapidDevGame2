using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.StateMachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
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
    
    [field:SerializeField]public NPCBaseState currentState{ get; private set; }
    
    [field:SerializeField]public bool FoundItems{ get; private set; }
    [field:SerializeField]public TextIndex TextIndex{ get; private set; }
    
    [field:SerializeField]public String FoundText{ get; private set; }
    
    [field:SerializeField]public String OpeningLine{ get; private set; }
    
    [field:SerializeField]public String NotFoundText{ get; private set; }
    
    [field:SerializeField]public List<ItemTypeSo> Items{ get; private set; } = new();

    [field: SerializeField] public List<ItemTypeSo> ItemsCollected{ get; private set; } = new();

    public event Action<List<ItemTypeSo>> ArrivedAtCheckout;
    
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
    
    [field:SerializeField] public int TimeForFirstWander { get; set; }
    
    [field:SerializeField] public int TimeToLeave { get; private set; }
    
    [field:SerializeField] public bool Shoplifter { get; private set; }
    
    [field:SerializeField] public bool CanTalk { get; set; }
    
    [field:SerializeField] public bool CanTarget { get; set; }

    
    [field:SerializeField] public SpriteRenderer Renderer { get; private set; }
    
    [field:SerializeField] public List<Image> PossibleImages{ get; private set; }

    [field:SerializeField] public List<Image> PreviousImages{ get; private set; }
    
    [field:SerializeField] public Shelf CurrentShelf{ get; set; }
    
    [field:SerializeField] public UiHide UiHide{ get; set; }
    
    [field:SerializeField]public List<Sprite> PossibleBad{ get;  set; }
    
    [field:SerializeField]public List<Sprite> PossibleGood{ get;  set; }
    
    [field:SerializeField]public List<Sprite> PossibleOpening{ get;  set; }
    
    [field:SerializeField]public Sprite CurrentSprite{ get;  set; }
    
    private bool _ranBefore;

    private bool _clicked;

    private bool _interacted;

    private int _numberOfItems = 0;

    private EventManager _eventManager;

    [SerializeField]private Image _placeHolder;

    public SwipeTask SwipeTask;

    
    
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
        _eventManager = FindFirstObjectByType<EventManager>();
        PossibleBad = NpcType.PossibleBad;
        PossibleGood = NpcType.PossibleGood;
        PossibleOpening = NpcType.PossibleOpening;
        SwipeTask = FindFirstObjectByType<SwipeTask>();
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

    public void ArrivedEvent()
    {
        if(SwipeTask.CheckingOut)return;
        _eventManager.InvokeArrived(ItemsCollected);
        _eventManager.AssignNpc(this);
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

    public void GiveUp()
    {
        if (ShelvesBeforeLeave >= 0)return;

        if (ItemsCollected.Count >= 1)
        {
            ChangeState(NpcStateName.Checkout);
        }
        else if (ItemsCollected.Count <= 0)
        {
            ChangeState(NpcStateName.Exit);
        }

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
        ShowImages();
        TextIndex.StartCoroutine("ImageVisible");
    }

    public void ChangeToPositive()
    {
        ShowImages();
        TextIndex.StartCoroutine("ImageVisible");
    }

    public void ShowOpening()
    {
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
        
        
        foreach (var item in Items)
        {
            _numberOfItems = Items.Count;
            for (int i = 0; i <= _numberOfItems -1; i++)
            {
                if (shelf.RowsOfShelves[shelf.ShelfSelected] == null ) continue;
                if(shelf.RowsOfShelves[shelf.ShelfSelected].ItemCount <= 0) continue;
                if (Shoplifter)
                {
                    ItemsCollected.Add(item);// add the item to the npcs list of collected items
                    MoneySpent += shelf.RowsOfShelves[shelf.ShelfSelected].ItemType.Cost;// spend the money
                    shelf.RowsOfShelves[shelf.ShelfSelected].ItemCount--;
                    ChangeState(NpcStateName.Exit);
                
                }
                else if (shelf.RowsOfShelves[shelf.ShelfSelected].ItemType != item||Budget < shelf.RowsOfShelves[shelf.ShelfSelected].ItemType.Cost)
                {
                    ShelvesBeforeLeave--;// decrement the amount of shelves it takes before the npc decides to leave empty handed 
                    continue;
                }
                else
                {
                    ItemsCollected.Add(item);// add the item to the npcs list of collected items
                    MoneySpent += shelf.RowsOfShelves[shelf.ShelfSelected].ItemType.Cost;// spend the money
                    shelf.RowsOfShelves[shelf.ShelfSelected].ItemCount--;
                    ShelvesBeforeLeave--;
                }
                shelf.ShelfSelected++;
            }

            shelf.ShelfSelected = 0;
            
            if (ItemsCollected.Count >= _numberOfItems)
            {
                FoundItems = true;
                ChangeState(_npcPositiveDialogState);
                return;
            }
            else
            {
                FoundItems = false;
                ChangeState(_npcNegativeDialogState);
                return;
            }
        }
        
        // remove the shelf from the list so the npc does not try to go to it again
    }
    public void DistanceCheck()// check the distance between shelfs and the npc, if within distance switch state
    {
        if (ArrivedAtTarget())
        {
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
                currentState = _npcCheckoutState;
                CanTarget = false;
                break;
            case NpcStateName.PositiveDialog:
                base.ChangeState(_npcPositiveDialogState);
                currentState = _npcPositiveDialogState;
                CanTarget = false;
                break;
            case NpcStateName.Enter:
                base.ChangeState(_npcEnterState);
                currentState = _npcEnterState;
                CanTarget = true;
                break;
            case NpcStateName.Exit:
                base.ChangeState(_npcExitState);
                currentState = _npcExitState;
                CanTarget = false;
                break;
            case NpcStateName.CheckShelf:
                base.ChangeState(_npcShelfCheckState);
                currentState = _npcShelfCheckState;
                CanTarget = false;
                break;
            case NpcStateName.Wander:
                base.ChangeState(_npcWanderState);
                currentState = _npcWanderState;
                CanTarget = false;
                break;
            case NpcStateName.NegativeDialog:
                base.ChangeState(_npcNegativeDialogState);
                currentState = _npcNegativeDialogState;
                CanTarget = false;
                break;
            case NpcStateName.Spawn:
                base.ChangeState(_npcSpawnState);
                currentState = _npcSpawnState;
                CanTarget = false;
                break;
            case NpcStateName.Talking:
                base.ChangeState(_npcTalkingState);
                currentState = _npcTalkingState;
                CanTarget = true;
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

    public virtual void RandomizeImage(Sprite sprite)
    {
        int i = 0;
        if (PossibleImages.Count < PossibleOpening.Count)
        {
            PossibleImages[i].sprite = sprite;
            PossibleImages.Add(_placeHolder);
            PossibleImages[i].sprite = sprite;
        }
        else if (PossibleImages.Count >= PossibleOpening.Count)
        {
            PossibleImages.Clear();
            PossibleImages.Add(_placeHolder);
            PossibleImages[i].sprite = sprite;
        }

    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
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
