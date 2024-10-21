using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStocker : MonoBehaviour
{
    public bool StockModeEnabled { get; set; }
    
    private MousePositionHandler _positionHandler;

    private void Start()
    {
        _positionHandler = GetComponent<MousePositionHandler>();
    }

    public void CheckForShelfToStock()
    {
        var shelf = _positionHandler.CheckForObjectToInteract().GetComponent<Shelf>();
    }
}
