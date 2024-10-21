using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemSelector : MonoBehaviour
{
    
    [SerializeField] private InventoryContainter[] allItems;
    
    public ItemSelectorUI UI;
    public event Action<InventoryContainter> OnItemLoad;
    public event Action<InventoryContainter> OnItemSelected;
    public event Action<InventoryContainter> OnItemChosen;
    private InventoryContainter _selectedItem;
    private InventoryContainter _chosenItem;
    int i = 0;

    public void LoadItems()
    {
        
        foreach (var itemData in allItems)
        {
            OnItemLoad?.Invoke(itemData);
        }
    }
    public void reLoadItems()
    {
        i = 0;
        foreach(var Transform in UI.itemButtonPanel)
        {
            UnityEngine.Object.Destroy(UI.itemButtonPanelGameObject.transform.GetChild(i).gameObject);
            i++;
            
        }
        foreach (var itemData in allItems)
        {
            OnItemLoad?.Invoke(itemData);
        }
    }


    //exec on item button press
    public void SelectItem(InventoryContainter itemData)
    {
        _selectedItem = itemData;

        OnItemSelected?.Invoke(_selectedItem);

    }

    // Start is called before the first frame update
    void Start()
    {
        LoadItems();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
