using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaceModeButton : MonoBehaviour
{
    private StoreObjectSO _assignedObject;
    
    [SerializeField] private Image buttonImage;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemCost;

    public void AssignContainer(StoreObjectSO container)
    {
        _assignedObject = container;
        buttonImage.sprite = _assignedObject.MenuSprite;
        itemName.text = _assignedObject.ObjectName;
        itemCost.text = $"${_assignedObject.ObjectPrice:N}";
    }
}
