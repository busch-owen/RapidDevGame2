using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ItemSelector : MonoBehaviour
{
    
    [field: SerializeField] public List<GameContainer> AllItems { get; private set; }
    
    public ItemSelectorUI UI;
    public event Action<GameContainer> OnItemLoad;
    public event Action<GameContainer> OnItemSelected;
    public event Action<GameContainer> OnItemChosen;
    private GameContainer _selectedItem;
    private GameContainer _chosenItem;
    int i = 0;

    public void LoadItems()
    {
        
        foreach (var itemData in AllItems)
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
        foreach (var itemData in AllItems)
        {
            OnItemLoad?.Invoke(itemData);
        }
    }


    //exec on item button press
    public void SelectItem(GameContainer itemData)
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
