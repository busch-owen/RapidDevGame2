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
    [field:SerializeField]public int Cost{ get; private set; }
}
