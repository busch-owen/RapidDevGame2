using System;
using UnityEngine;

[Serializable]
public class InventoryContainter
{
    [field: SerializeField] public string ItemName { get; private set; }
    [field: SerializeField] public int GameID { get; private set; }
    [field: SerializeField] public ItemTypeSo ItemType { get; private set; }
    [field: SerializeField] public int ItemCount { get; private set; }

    public void ChangeCount(int amt)
    {
        ItemCount += amt;
    }
}
