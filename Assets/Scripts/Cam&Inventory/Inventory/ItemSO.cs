using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameItem", menuName = "SOs/GameInvItem")]

public class ItemSO : ScriptableObject
{
    [SerializeField] public string itemName;
    [SerializeField] public string itemDescription;
    [SerializeField] public string itemCategory;
    [SerializeField] public int itemPrice;
    [SerializeField] public Sprite bigIcon;
    [SerializeField] public Sprite smallIcon;

}
