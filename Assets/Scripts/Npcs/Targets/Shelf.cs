using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shelf : StoreObject
{
    [field: SerializeField] public GameContainer AssignedItem { get; private set; }
    
    [field: SerializeField] public Image Image { get; private set; }
    
    [field: SerializeField] public List<Rows> Rows { get; private set; }
    
    [field: SerializeField] public Rows AssignedRow { get;  set; }
    
    [field: SerializeField] public Transform[] rows { get; private set; }

    [SerializeField] private Rows _row;

    [SerializeField] private GameObject _rowToIns;
    
    private ItemSelector _assignedSelector;
    [SerializeField] private int _index = 0;
    [field: SerializeField] public NpcStateMachine StateMachine { get; private set; }
    [field: SerializeField] public EmployeeStateMachine EmpStateMachine { get; private set; }
    public bool IsFlashing;
    private ShelfClick _shelfClick;
    private EventManager _eventManager;
    private Coroutine _flashRoutine;
    public int ShelfSelected = 0;
    private bool _firstPress = true;
    public Transform Target;
    public GameObject ShelfUI;
    public ItemSelector ItemSelector;

    private ItemTypeSo _itemTypeSo;
    private ItemTypeSo _currentItem;
    public StockOrderer stockOrderer;
    public InvGrid grid;
    [field: SerializeField] public int stock { get; private set; }
    [field: SerializeField] public GameContainer CurrentContainer { get; private set; }
    
    [field: SerializeField] public int[] StockPerRow { get; private set; }
    
   

    public override void Start()
    {
        _shelfClick = GetComponentInChildren<ShelfClick>();
        _shelfClick.image.enabled = false;
        Renderer = GetComponentInChildren<SpriteRenderer>();
        _eventManager = FindFirstObjectByType<EventManager>();
        _eventManager._clicked += DisableClick;
        EmpStateMachine = FindFirstObjectByType<EmployeeStateMachine>();
        Target = EmpStateMachine.transform;
        ItemSelector = FindFirstObjectByType<ItemSelector>();
        stockOrderer = FindFirstObjectByType<StockOrderer>();
        grid = FindFirstObjectByType<InvGrid>();
        InstantiateRows();
    }

    public void StockShelf(ItemTypeSo item)
    {
        CurrentContainer = AssignedRow.Container;
        var trans = rows[AssignedRow.index];

        if (CurrentContainer.ItemType != null && CurrentContainer.ItemCount >= AssignedRow.Container.ItemType.MaxCount) return;

        if(CurrentContainer.ItemType != item) return;
        
        if( StockPerRow[_index] >= CurrentContainer.ItemType.MaxCount) return;   
        
        StockPerRow[_index]++;
                    
        CurrentContainer.ItemType = item;
        _itemTypeSo = CurrentContainer.ItemType;
        CurrentContainer.ItemName = item.ItemName;
        CurrentContainer.GameID = item.GameID;
        Image.sprite = CurrentContainer.ItemType.BigIcon;
        var Img = Instantiate(Image, trans);
        Img.transform.parent = trans;
    }

    public void FirstStock()
    {
        
    }

    public void InstantiateRows()
    {
        if(_rowToIns == null) return;
        foreach (var trans in rows)
        {
            int i = _index;
            var RowToInstantiate = Instantiate(_rowToIns, rows[i]);
            
            RowToInstantiate.GetComponent<Rows>().index = _index;
            Rows.Add(RowToInstantiate.GetComponent<Rows>());
            _index++;
        }

        AssignedRow = Rows[0];
    }

    public void RemoveStock(ItemTypeSo item)
    {
        var CurrentContainer = AssignedRow.Container;
        
        if(item != CurrentContainer.ItemType)return;
        CurrentContainer.ItemCount--;
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

    public void EnableClick()
    {
        _shelfClick.image.enabled = true;
    }

    public void DisableClick()
    {
        IsFlashing = false;
        if(_flashRoutine == null) return;
        StopCoroutine(_flashRoutine);
        Renderer.color = Color.white;
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

    public void SetItem(ItemTypeSo itemSO)
    {
        
    }

    public void UnstockShelf()
    {
        _assignedSelector?.AllItems.Find(container => container.GameID == AssignedItem.GameID).ChangeCount(AssignedItem.ItemCount);
        _assignedSelector = null;
        AssignedItem = null;
    }

    public void AssignRows(int index)
    {
        if(AssignedRow.Container.ItemType != null && AssignedRow.Container.ItemCount < AssignedRow.Container.ItemType.MaxCount) return;
        AssignedRow = Rows[index];
        _index = index;
        Debug.Log(index);
        _firstPress = true;
    }

    public void AssignNpc(NpcStateMachine stateMachine)
    {
        StateMachine = stateMachine;
        StateMachine.SelectedEvent += FlashColor;
    }
}
