using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Clicked();

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

    public event Action<List<ItemTypeSo>> Arrived;

    public event Action<NpcStateMachine> Npc;

    public event Clicked _clicked;

    public List<ItemTypeSo> items;

    public void InvokeArrived(List<ItemTypeSo> ItemList)
    {
        Arrived?.Invoke(ItemList);
        items = ItemList;
    }

    public void AssignNpc(NpcStateMachine npc)
    {
        Npc?.Invoke(npc);
    }

    public void InvokeClicked()
    {
        _clicked?.Invoke();
    }
}
