using System.Collections;
using System;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;

public class ObjectDeleter : MonoBehaviour
{
    public bool DeleteModeEnabled { get; set; }
    
    private MousePositionHandler _positionHandler;

    private ObjectMover _objectMover;

    private NavMeshSurface _surface;
    private MoneyManager _moneyManager;

    private ItemSelector _itemSelector;
    private TutorialHandler _tutorial;

    public event Action ObjectDeleted;

    private void Start()
    {
        _positionHandler = GetComponent<MousePositionHandler>();
        _objectMover = GetComponent<ObjectMover>();
        _moneyManager = FindFirstObjectByType<MoneyManager>();
        _itemSelector = FindFirstObjectByType<ItemSelector>();
        _surface = FindFirstObjectByType<NavMeshSurface>();
        _tutorial = FindFirstObjectByType<TutorialHandler>();
        if (!_tutorial) return;
        ObjectDeleted += _tutorial.ChangeSequenceIndex;
    }   

    public void CheckForObjectToDelete()
    {
        if (_positionHandler.CheckForObjectToInteract() == null || !DeleteModeEnabled || TutorialHandler.InDialogue) return;
        var tempObject = _positionHandler.CheckForObjectToInteract();
        _surface.BuildNavMesh();
        _moneyManager.IncrementProfit(tempObject.AssignedObject.ObjectPrice / 3);
        
        Destroy(tempObject.gameObject);
        ObjectDeleted?.Invoke();
        if (!_tutorial) return;
        ObjectDeleted -= _tutorial.ChangeSequenceIndex;
    }

    public void ToggleDeleteMode()
    {
        DeleteModeEnabled = !DeleteModeEnabled;
    }
}
