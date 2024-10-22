using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemButton : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text itemName;

    public void SetButton(GameContainer itemData)
    {
        itemName.text = itemData.ItemType.ItemName;
        itemImage.sprite = itemData.ItemType.SmallIcon;
    }

}
