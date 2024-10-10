using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [field: SerializeField] public ItemTypeSo AssignedItem { get; private set; }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var target = other.GetComponent<NPCAI>();
        if (target)
        {
            Debug.Log(target);
            target.ChooseTarget();
        }
    }

}
