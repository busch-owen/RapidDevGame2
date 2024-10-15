using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemButton : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text itemName;

    public void SetButton(ItemSO itemData)
    {
        itemImage.sprite = itemData.icon;
        itemName.text = itemData.itemName;
    }

}
