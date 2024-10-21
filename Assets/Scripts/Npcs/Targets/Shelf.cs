using UnityEngine;

public class Shelf : StoreObject
{
    [field: SerializeField] public InventoryContainter AssignedItem { get; private set; }
    
    public void StockShelf(int id, ItemSelector selector)
    {
        AssignedItem = selector.AllItems.Find(container => container.GameID == id);
        if (AssignedObject.StockAmount < AssignedItem.ItemCount)
        {
            AssignedItem.SetCount(AssignedObject.StockAmount);
        }
        else
        {
            AssignedItem.SetCount(AssignedItem.ItemCount);
        }
    }

    public void UnstockShelf()
    {
        AssignedItem = null;
    }
}
