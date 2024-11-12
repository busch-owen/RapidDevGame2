using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvGrid : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform gridLayout;

    [SerializeField] private GameObject _itemButton;

    private ItemSelector _itemSelector;

    private GameContainer _container;
    
    private void Start()
    {
        gridLayout = GetComponentInChildren<GridLayoutGroup>().transform;
        
        transform.localScale = new Vector3(0, 0, 0);
        InstantiateButtons();
    }

    public void EnableImage(Shelf shelf)
    {
        transform.localScale = new Vector3(5.495245f,10.74701f,1);
        var newButtons = GetComponentsInChildren<StockItemButton>();
        foreach (var button in newButtons)
        {
            button.AssignShelf(shelf);
            
        }
    }

    public void InstantiateButtons()
    {
        _itemSelector = FindFirstObjectByType<ItemSelector>();

        foreach (var container in _itemSelector.AllItems)
        {
            var newObj = Instantiate(_itemButton, gridLayout);
            newObj.GetComponent<StockItemButton>().AssignInventoryContainer(container);
        }
    }
    
    public void DisableImage()
    {
        transform.localScale = new Vector3(0,0,0);
    }
}
