using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    private ItemSelector _itemSelector;
    [SerializeField] private GameObject inventoryObject;
    [SerializeField] private Transform inventoryGrid;

    private void Start()
    {
        _itemSelector = FindFirstObjectByType<ItemSelector>();
        OccupyMenu();
    }

    private void OccupyMenu()
    {
        foreach (var item in _itemSelector.AllItems)
        {
            var newObject = Instantiate(inventoryObject, inventoryGrid);
            newObject.GetComponent<InventoryObject>().AssignContainer(item);
        }
    }
}
