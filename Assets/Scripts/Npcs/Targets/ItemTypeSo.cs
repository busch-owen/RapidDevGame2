using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProductType
{
    Console, Game, Controller, Collectable, Apparel
}

[CreateAssetMenu(menuName = "My Assets/ItemTypeSo")]

public class ItemTypeSo : ScriptableObject
{
    [field: SerializeField] public int Cost { get; private set; }
    [field: SerializeField] public string ItemName { get; private set; }
    [field: SerializeField] public string ItemDescription { get; private set; }
    [field: SerializeField] public string ItemCategory { get; private set; }
    [field: SerializeField] public ProductType ProductType { get; private set; }
    [field: SerializeField] public Sprite BigIcon { get; private set; }
    [field: SerializeField] public Sprite SmallIcon { get; private set; }
}
