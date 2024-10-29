using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shelf : StoreObject
{
    [field: SerializeField] public GameContainer AssignedItem { get; private set; }
    [field: SerializeField] public GameContainer[] RowsOfShelves { get; private set; }
    private ItemSelector _assignedSelector;
    [field: SerializeField] public NpcStateMachine StateMachine { get; private set; }
    [field: SerializeField] public EmployeeStateMachine EmpStateMachine { get; private set; }
    public bool IsFlashing;
    private ShelfClick _shelfClick;
    private EventManager _eventManager;
    private Coroutine _flashRoutine;
    public int ShelfSelected = 0;
    private bool _firstPress;
    public Transform Target;
    public GameObject ShelfUI;
    
   

    public override void Start()
    {
        _shelfClick = GetComponentInChildren<ShelfClick>();
        _shelfClick.image.enabled = false;
        Renderer = GetComponentInChildren<SpriteRenderer>();
        _eventManager = FindFirstObjectByType<EventManager>();
        _eventManager._clicked += DisableClick;
        EmpStateMachine = FindFirstObjectByType<EmployeeStateMachine>();
        Target = EmpStateMachine.transform;
        ShelfUI = FindFirstObjectByType<ShelfUi>().gameObject;
        ShelfUI.transform.localScale = new Vector3(0,0,0);
    }
    
    public void StockShelf(ItemTypeSo item)
    {
        var CurrentContainer = RowsOfShelves[ShelfSelected];

        if (!_firstPress)
        {
            CurrentContainer.ItemType = item;
            CurrentContainer.ItemName = item.ItemName;
            CurrentContainer.GameID = item.GameID;
            CurrentContainer.ItemCount++;
            _firstPress = true;
        }
        else
        {
            CurrentContainer.ItemCount++;
        }

        
    }
    
    public bool DistanceCheck()
    {
        if (Vector2.Distance(transform.position, Target.position) <= 2.0f)
        {
            return true;
        }
        return false;
    }

    public void SelectShelf()
    {
        ShelfUI.transform.localScale = new Vector3(1,1,1);
    }

    public void DeselectShelf()
    {
        ShelfUI.transform.localScale = new Vector3(0,0,0);
    }

    public void IncrementShelf()
    {
        if(ShelfSelected >= RowsOfShelves.Length) ShelfSelected = 0;
        ShelfSelected++;
    }

    public void EnableClick()
    {
        _shelfClick.image.enabled = true;
    }

    public void DisableClick()
    {
        IsFlashing = false;
        if(_flashRoutine == null) return;
        StopCoroutine(_flashRoutine);
    }

    public void FlashColor()
    {
        if (!IsFlashing)
        {
            IsFlashing = true;
            _flashRoutine = StartCoroutine(Flash());
        }
        else
        {
            IsFlashing = false;
            Renderer.color = Color.white;
            StopCoroutine(_flashRoutine);
        }

    }

    private IEnumerator Flash()
    {
        while (IsFlashing)
        {
            Renderer.color = Color.green;
            yield return new WaitForSeconds(0.5f);
            Renderer.color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }

        yield return null;

    }

    public void UnstockShelf()
    {
        _assignedSelector?.AllItems.Find(container => container.GameID == AssignedItem.GameID).ChangeCount(AssignedItem.ItemCount);
        _assignedSelector = null;
        AssignedItem = null;
    }

    public void AssignNpc(NpcStateMachine stateMachine)
    {
        StateMachine = stateMachine;
        StateMachine.SelectedEvent += FlashColor;
    }
}
