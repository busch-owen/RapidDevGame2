using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewBehaviourScript : MonoBehaviour, IPointerClickHandler
{
    private Shelf _shelf;

    private void Start()
    {
        _shelf = GetComponentInParent<Shelf>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _shelf.IncrementShelf();
    }
}
