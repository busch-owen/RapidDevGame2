using System.Collections;
using System.Linq;
using UnityEngine;

public class Shelf : StoreObject
{
    [field: SerializeField] public GameContainer AssignedItem { get; private set; }
    private ItemSelector _assignedSelector;
    private NpcStateMachine _stateMachine;
    public bool IsFlashing;
    
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

    public void FlashColor()
    {
        if (!IsFlashing)
        {
            IsFlashing = true;
            StartCoroutine("Flash");
        }
        else
        {
            IsFlashing = false;
            Renderer.color = Color.white;
            StopCoroutine("Flash");
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
        _stateMachine = stateMachine;
        _stateMachine.SelectedEvent += FlashColor;
    }
}
