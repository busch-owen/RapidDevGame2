using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shelf : StoreObject
{
    private NPCAI target;
    public float timeToNextTarget;
    
    [field: SerializeField] public ItemTypeSo AssignedItem { get; private set; }

    [SerializeField] public List<ItemTypeSo> possibleItems = new List<ItemTypeSo>();

    public void OnTriggerEnter2D(Collider2D other)
    {
        target = other.GetComponent<NPCAI>();
        if (target)
        {
            Debug.Log(target);
            Invoke("TargetChange", timeToNextTarget);
        }
    }

    private void Start()
    {
        var itemToSet = Random.Range(0, possibleItems.Count);
        if(possibleItems.Count < 0)
            AssignedItem = possibleItems[itemToSet];
    }
    
    private void TargetChange()
    {
        target.ChooseTarget();
    }
}
