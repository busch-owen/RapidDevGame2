using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StockItemButton : MonoBehaviour
{
    private GameContainer _assignedContainer;
    
    [SerializeField] private Image buttonImage;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemCount;
    [SerializeField] public ItemTypeSo Item;
    private Shelf _shelf;
    private EventManager _eventManager;

    public void AssignInventoryContainer(GameContainer container)
    {
        _assignedContainer = container;
        buttonImage.sprite = _assignedContainer.ItemType.BigIcon;
        itemName.text = _assignedContainer.ItemType.ItemName;
        Item = _assignedContainer.ItemType;
    }

    public void Stock()
    {
        _shelf.AssignedRow.Container = this._assignedContainer;
        _shelf.StockShelf(Item);
    }

    public void assignShelf(Shelf shelf)
    {
        _shelf = shelf;
    }

    private void Start()
    {
        _eventManager = FindFirstObjectByType<EventManager>();
        _eventManager.ShelfAssigned += assignShelf;
    }
}
