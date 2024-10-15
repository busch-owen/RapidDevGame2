using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "SOs/Inv")]

public class ItemSO : ScriptableObject
{
    [SerializeField] public int id;
    [SerializeField] public int Stats;
    [SerializeField] public string itemName;
    [SerializeField] public string itemDescription;
    [SerializeField] public Sprite icon;
    [SerializeField] public GameObject worldItem;
    
  
    
}
