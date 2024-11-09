using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Clicked();
public delegate void DayEnd();

public class EventManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public event Action<ItemTypeSo> Arrived;

    public event Action<NpcStateMachine> Npc;

    public event Action<Shelf> ShelfAssigned;

    public event Clicked _clicked;

    public event DayEnd _Ended;

    public ItemTypeSo Item;

    public void InvokeArrived(ItemTypeSo item)
    {
        Arrived?.Invoke(item);
        Item = item;
    }
    
    
    

    public void AssignNpc(NpcStateMachine npc)
    {
        Npc?.Invoke(npc);
    }

    public void InvokeClicked()
    {
        _clicked?.Invoke();
    }

    public void InvokeEndDay()
    {
        _Ended?.Invoke();
    }
}
