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

    private ItemSelector _itemSelector;
    

    public void AssignInventoryContainer(GameContainer container)
    {
        _assignedContainer = container;
        buttonImage.sprite = _assignedContainer.ItemType.BigIcon;
        itemName.text = _assignedContainer.ItemType.ItemName;
        Item = _assignedContainer.ItemType;
    }

    public void Stock()
    {
        if(_assignedContainer.ItemCount <= 0 && _assignedContainer.ItemType.MaxCount >= _shelf.AssignedRow.Container.ItemCount) return;

        if (_shelf.AssignedRow.Container.ItemType == null)
        {
            _shelf.AssignedRow.Container.ItemName = _assignedContainer.ItemName;
            _shelf.AssignedRow.Container.ItemType = _assignedContainer.ItemType;
        }
        
        if (_assignedContainer.ItemType == _shelf.AssignedRow.Container.ItemType)
        {
            Debug.Log(_shelf.AssignedRow.Container.ItemCount);
            if( _shelf.AssignedRow.Container.ItemCount >= _assignedContainer.ItemType.MaxCount) return;
            _shelf.AssignedRow.Container.ChangeCount(1);
            _assignedContainer.ChangeCount(-1);
            _shelf.StockShelf(Item);
        }
    }

    public void FirstStock()
    {
        
    }

    public void AssignShelf(Shelf shelf)
    {
        Debug.Log("I HAVE A SHELF!");
        _shelf = shelf;
    }

    private void Start()
    {
        _eventManager = FindFirstObjectByType<EventManager>();
        _itemSelector = FindFirstObjectByType<ItemSelector>();
        
    }
}
