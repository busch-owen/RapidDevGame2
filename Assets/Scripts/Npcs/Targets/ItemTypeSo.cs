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
    [field: SerializeField] public string itemName { get; private set; }
    [field: SerializeField] public string itemDescription { get; private set; }
    [field: SerializeField] public string itemCategory { get; private set; }
    [field: SerializeField] public int itemPrice { get; private set; }
    [field: SerializeField] public Sprite bigIcon { get; private set; }
    [field: SerializeField] public Sprite smallIcon { get; private set; }
}
