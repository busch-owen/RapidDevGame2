using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shelf : StoreObject
{
    [field: SerializeField] public GameContainer AssignedItem { get; private set; }
    private ItemSelector _assignedSelector;
    [field: SerializeField] public NpcStateMachine StateMachine { get; private set; }
    public bool IsFlashing;
    private ShelfClick _shelfClick;
    private EventManager _eventManager;
    
    public void StockShelf(int id, ItemSelector selector)
    {
        _assignedSelector = selector;
        var tempItem = selector.AllItems.Find(container => container.GameID == id);
        
        AssignedItem = new GameContainer
        {
            ItemName = tempItem.ItemName,
            GameID = tempItem.GameID,
            ItemType = tempItem.ItemType
        };

        if(tempItem.ItemCount == 0) return;
        
        if (AssignedObject.StockAmount < tempItem.ItemCount)
        {
            AssignedItem.SetCount(AssignedObject.StockAmount);
            Debug.LogFormat($"Space on shelf is less than stock available.");
            tempItem.ChangeCount(-AssignedObject.StockAmount);
        }
        else
        {
            AssignedItem.SetCount(tempItem.ItemCount);
            tempItem.ItemCount = 0;
            Debug.LogFormat($"Stock available is less than space on shelf.");
        }
    }

    public override void Start()
    {
        _shelfClick = GetComponentInChildren<ShelfClick>();
        _shelfClick.image.enabled = false;
        Renderer = GetComponentInChildren<SpriteRenderer>();
        _eventManager = FindFirstObjectByType<EventManager>();
    }

    public void EnableClick()
    {
        _shelfClick.image.enabled = true;
    }

    public void DisableClick()
    {
        //Renderer = GetComponentInChildren<SpriteRenderer>();
        IsFlashing = false;
        //Renderer.color = Color.white;
        StopCoroutine("Flash");
    }

    public void FlashColor()
    {
        if (!IsFlashing)
        {
            _eventManager._clicked += DisableClick;
            IsFlashing = true;
            StartCoroutine("Flash");
        }
        else
        {
            _eventManager._clicked += DisableClick;
            IsFlashing = false;
            Renderer.color = Color.white;
            StopAllCoroutines();
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
