using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shelf : StoreObject
{

    private NPCAI target;
    public float timeToNextTarget;
    [field: SerializeField] public ItemTypeSo AssignedItem { get; private set; }

    public void OnTriggerEnter2D(Collider2D other)
    {
        target = other.GetComponent<NPCAI>();
        if (target)
        {
            Debug.Log(target);
            Invoke("TargetChange", timeToNextTarget);
        }
    }


    private void TargetChange()
    {
        target.ChooseTarget();
    }
}
