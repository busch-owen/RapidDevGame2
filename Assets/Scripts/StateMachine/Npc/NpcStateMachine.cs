using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.StateMachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public enum NpcStateName
{
    Enter,Wander,CheckShelf,CorrectItem,IncorrectItem,PositiveDialog,Checkout,Exit,NegativeDialog,Spawn,Talking,KickedOut
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
    private NpcKickedOutState _npcKickedOutState;
    private Shelf _shelfToCheck;
    private ItemSelector _itemSelector;

    public event Selected SelectedEvent;
    
    [field:SerializeField]public NPCBaseState currentState{ get; private set; }
    
    [field:SerializeField]public bool FoundItems{ get; private set; }
    [field:SerializeField]public TextIndex TextIndex{ get; private set; }
    
    [field:SerializeField]public String FoundText{ get; private set; }
    
    [field:SerializeField]public String OpeningLine{ get; private set; }
    
    [field:SerializeField]public String NotFoundText{ get; private set; }
    
    [field:SerializeField]public ItemTypeSo ItemWanted{ get; private set; }

    [field: SerializeField] public ItemTypeSo ItemCollected{ get; private set; }

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
    
    [field:SerializeField]public int ShelvesBeforeLeave { get;  set; }
    
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
    
    [field:SerializeField]public Sprite Opening{ get;  set; }
    
    [field:SerializeField]public Sprite CurrentSprite{ get;  set; }

    public EmployeeStateMachine EmployeeStateMachine;

    public bool BeingKickedOut;
    
    private bool _ranBefore;

    private bool _clicked;

    private bool _interacted;

    private int _numberOfItems = 0;

    [SerializeField]private Image[] imgs;

    private EventManager _eventManager;

    [SerializeField]private Image _placeHolder;

    public SwipeTask SwipeTask;

    private Sprite _randomSprite;

    [SerializeField]private Rows[] _rows;
    
    int i = 0;
    
    private List<GameObject> instantiated;

    private string stateName;

    public Animator Animator;

    [SerializeField] private AudioClip positiveClip, negativeClip;

    
    
    // Start is called before the first frame update
    void Start()
    {
        Agent.updateRotation = false;
        Agent.speed = NpcType.Speed;
        Budget = NpcType.Budget;
        MoneyManager = FindFirstObjectByType<MoneyManager>();
        Shoplifter = NpcType.ShopLifter;
        Renderer = GetComponentInChildren<SpriteRenderer>();
        Renderer.sprite = NpcType.NpcSprite;
        Animator = gameObject.AddComponent<Animator>();
        Animator.runtimeAnimatorController = NpcType.Animator;
        Renderer.color = Color.white;
        Renderer.transform.rotation = Quaternion.Euler(90,0,0);
        _eventManager = FindFirstObjectByType<EventManager>();
        PossibleBad = NpcType.PossibleBad;
        PossibleGood = NpcType.PossibleGood;
        SwipeTask = FindFirstObjectByType<SwipeTask>();
        _itemSelector = FindFirstObjectByType<ItemSelector>();
        
        var rand = Random.Range(0, NpcType.PossibleItems.Count);
        ItemWanted = NpcType.PossibleItems[rand];
        _randomSprite = NpcType.PossibleItems[rand].GameEmoji;
        Opening = _randomSprite;

        if (ShelvesBeforeLeave > Shelves.Count)
        {
            ShelvesBeforeLeave = Shelves.Count;
        }
        else
        {
            ShelvesBeforeLeave = NpcType.ShelvesTillExit;
        }
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
        _npcKickedOutState = new NpcKickedOutState(this);
        Exit = FindFirstObjectByType<Exit>().transform;
        AssignShelves();
        AssignRegisters();
        ChooseNpc();
        TextIndex = GetComponentInChildren<TextIndex>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        PossibleImages.Add(_placeHolder);
        ChangeState(_npcSpawnState);
    }

    public void ArrivedEvent()
    {
        if(SwipeTask.CheckingOut)return;
        _eventManager.InvokeArrived(ItemCollected);
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

        if (ItemCollected == ItemWanted)
        {
            ChangeState(NpcStateName.Checkout);
            ReputationManager.ChangeReputation(0.005f);
        }
        else if (ItemCollected == ItemWanted)
        {
            ChangeState(NpcStateName.Exit);
            ReputationManager.ChangeReputation(-0.01f);
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
        //Totals the odds of all the critters
        var totalWeight = NpcTypeSoOptions.Sum(npcType => npcType.OddsToSpawn);

        //Generates a random number from 0 to totalWeight
        var rand = Random.Range(0, totalWeight);
        
        foreach (var randomNpc in NpcTypeSoOptions)
        {
            //Checks if the random number is less than the current critter's pull chance
            if (rand <= randomNpc.OddsToSpawn)
            {
                NpcType = randomNpc;
                return;
            } 
            //if the random number is greater than the pull chance, it subtracts the pull chance from the random number and checks the next critter in the list
            rand -= randomNpc.OddsToSpawn;
        }
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

        _shelfToCheck = shelf;
        ShelvesBeforeLeave--;
        i = -1;

        foreach (var row in _shelfToCheck.Rows)
        {
            i++;
            _shelfToCheck.AssignedRow = _shelfToCheck.Rows[i];
            Debug.Log(row);
            if(_shelfToCheck.AssignedRow == null) return;
            if(_shelfToCheck.AssignedRow.Container.ItemCount <= 0) continue;

            if (ItemWanted == _shelfToCheck.AssignedRow.Container.ItemType)
            {
                ItemCollected =ItemWanted;
                MoneySpent += ItemWanted.Cost;

                foreach (var rows in _shelfToCheck.Rows)
                {
                    instantiated = new List<GameObject>();
                    var Images = _shelfToCheck.rows[_shelfToCheck.AssignedRow.index].GetComponentsInChildren<Image>();

                    foreach (var ImGs in Images)
                    {
                        instantiated.Add(ImGs.gameObject);
                    }

                    Debug.Log(Images.Length);
                    imgs = Images;

                    if (_shelfToCheck.AssignedRow.Container.ItemCount >= imgs.Length -1)
                    {
                        int d = _shelfToCheck.AssignedRow.Container.ItemCount -1;
                        Destroy(instantiated[d]);
                        _shelfToCheck.AssignedRow.Container.ItemCount--;
                    }

                }
            }

        }
        Shelves.Remove(shelf);

        if (ItemCollected == ItemWanted)
        {
            if (Shoplifter)
            {
                ChangeState(NpcStateName.Exit);
            }
            else
            {
                FoundItems = true;
                i = 0;
                ReputationManager.ChangeReputation(0.005f);
                ChangeState(_npcPositiveDialogState);
                SoundManager.PlayClip(positiveClip);
                return;
            }

        }
        else
        {
            FoundItems = false;
            i = 0;
            ChangeState(_npcNegativeDialogState);
            ReputationManager.ChangeReputation(-0.01f);
            SoundManager.PlayClip(negativeClip);
            return;
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
            case NpcStateName.KickedOut:
                base.ChangeState(_npcKickedOutState);
                CanTarget = false;
                currentState = _npcTalkingState;
                break;
        }
        
        this.stateName = currentState.ToString();
    }
    
    public void Wander()
    {
        ChangeState(NpcStateName.Wander);
    }

    public void AssignEmployee()
    {
        EmployeeStateMachine = FindFirstObjectByType<EmployeeStateMachine>();
    }

    public void Struggle()
    {
        Agent.enabled = false;
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

        PossibleImages[i].sprite = sprite;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInParent<EmployeeStateMachine>())
        {
            CanTalk = true;
            var Emp = other.GetComponentInParent<EmployeeStateMachine>();
            Emp.npcStateMachine = this;
            EmployeeStateMachine = Emp;

            if (Emp.KickingOut)
            {
                ChangeState(NpcStateName.KickedOut);
            }
        }
        
    }

    public void Die()
    {
        if (ItemCollected == ItemWanted && Shoplifter)
        {
            foreach (var inventoryItems in _itemSelector.AllItems.Where(inventoryItems => inventoryItems.ItemType == ItemCollected))
            {
                inventoryItems.ChangeCount(1);
            }
        }
        Destroy(this.gameObject);
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
