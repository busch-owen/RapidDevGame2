using UnityEngine;

[CreateAssetMenu(menuName = "BuschOwen/InventoryContainer")]
public class InventoryContainter : ScriptableObject
{
    [field: SerializeField] public ItemTypeSo ItemType { get; private set; }
    [field: SerializeField] public int ItemCount { get; private set; }

    public void ChangeCount(int amt)
    {
        ItemCount += amt;
    }
}
