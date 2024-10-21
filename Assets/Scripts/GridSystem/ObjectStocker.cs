using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectStocker : MonoBehaviour
{
    public bool StockModeEnabled { get; set; }
    
    private MousePositionHandler _positionHandler;

    private ItemSelector _selector;

    private Shelf _currentShelf;

    private StockInterface _interface;

    private bool _mouseOverUI;

    private void Start()
    {
        _positionHandler = GetComponent<MousePositionHandler>();
        _selector = FindFirstObjectByType<ItemSelector>();
        _interface = FindFirstObjectByType<StockInterface>();
        _interface.gameObject.SetActive(false);
    }

    private void Update()
    {
        _mouseOverUI = EventSystem.current.IsPointerOverGameObject();
    }

    public void ChangeModeState(bool newState)
    {
        StockModeEnabled = newState;
    }

    

    public void CheckForShelfToStock()
    {
        if (!StockModeEnabled || _mouseOverUI) return;
        if (_positionHandler.CheckForObjectToInteract().GetComponent<Shelf>() == null) return;
        _currentShelf = _positionHandler.CheckForObjectToInteract().GetComponent<Shelf>();
        _interface?.gameObject.SetActive(true);
    }

    public void AssignStock(int gameID)
    {
        _currentShelf.UnstockShelf();
        _currentShelf.StockShelf(gameID, _selector);
        _interface?.gameObject.SetActive(false);
    }
}
