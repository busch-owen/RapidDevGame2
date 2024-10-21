using System;
using UnityEngine;

[Serializable]
public class InventoryContainter
{
    [field: SerializeField] public string ItemName { get; set; }
    [field: SerializeField] public int GameID { get; set; }
    [field: SerializeField] public ItemTypeSo ItemType { get; set; }
    [field: SerializeField] public int ItemCount { get; set; }

    public void SetCount(int amt)
    {
        ItemCount = amt;
        Debug.LogFormat($"New amount is {ItemCount}");
    }

    public void ChangeCount(int amt)
    {
        ItemCount += amt;
        Debug.LogFormat($"New amount is {ItemCount}");
    }
}
