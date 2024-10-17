using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BuschOwen/Store Objects", fileName = "StoreObjectSO")]
public class ObjectDatabaseSO : ScriptableObject
{
    public List<ObjectData> objectsData;
}

[Serializable]
public class ObjectData
{
    [field: Header("Build Mode Attributes"), Space(10)]
    
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public GameObject PlaceModeObject { get; private set; }
    
    [field: SerializeField] public Vector2Int Size { get; private set; } = Vector2Int.one;
    [field: SerializeField] public GameObject ObjectToPlace { get; private set; }

    [field: Space(10), Header("Restocking Attributes"), Space(10)]
    
    [field: SerializeField] public bool ProductStorage { get; private set; } = true;
    [field: SerializeField] public ProductType[] CompatibleProducts { get; private set; }
    
    [field: Space(10), Header("Buy Menu Attributes"), Space(10)]
    
    [field: SerializeField] public Sprite MenuSprite { get; private set; }
    [field: SerializeField] public float ObjectPrice { get; private set; }
}