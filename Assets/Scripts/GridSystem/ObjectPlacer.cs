using System;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectPlacer : MonoBehaviour
{
    private StoreObjectSO _assignedObject;
    private GameObject _displayObject;
    public event Action ObjectPlaced;

    private ObjectPicker _picker;

    private GridPointer _pointer;
    
    private bool _cursorOnUI;

    private bool _placeModeEnabled = true;

    private bool _placeMultiple = true;

    private NavMeshSurface _surface;

    private MoneyManager _moneyManager;
    private ObjectMover _mover;

    private TutorialHandler _tutorial;

    private void Start()
    {
        _picker = FindFirstObjectByType<ObjectPicker>();
        _pointer = FindFirstObjectByType<GridPointer>();
        _surface = FindFirstObjectByType<NavMeshSurface>();
        _tutorial = FindFirstObjectByType<TutorialHandler>();
        _mover = GetComponent<ObjectMover>();
        _moneyManager = FindFirstObjectByType<MoneyManager>();
        if (!_tutorial) return;
        ObjectPlaced += _tutorial.ChangeSequenceIndex;
    }

    public void AssignObject(StoreObjectSO obj, GameObject displayObject)
    {
        _assignedObject = obj;
        _displayObject = displayObject;
        _placeModeEnabled = true;
    }

    private void Update()
    {
        _cursorOnUI = EventSystem.current.IsPointerOverGameObject();
    }

    public void ChangePlaceMode(bool newState)
    {
        _placeModeEnabled = newState;
    }

    public void PlaceMultiple(bool newState)
    {
        _placeMultiple = newState;
    }
    
    public void PlaceObject()
    {
        if(!_assignedObject || !_pointer.CursorOnGrid || _cursorOnUI || !_placeModeEnabled || TutorialHandler.InDialogue) return;
        
        var storeObject = _assignedObject.ObjectToPlace;
        if (_displayObject != null && !_displayObject.GetComponent<StoreObject>().Placeable)
        {
            return;
        }
        
        if (!_mover.MoveModeEnabled)
        {
            if (_moneyManager.Profit < _assignedObject.ObjectPrice) return;
            _moneyManager.DecrementProfit(_assignedObject.ObjectPrice);
        }
        else
        {
            _mover.ToggleMoveMode(false);
            _mover.Invoke(nameof(_mover.EnableMoveMode), 0.1f);
        }
        
        var newObject = Instantiate(storeObject, _pointer.CurrentCellPos, Quaternion.identity);
        newObject.GetComponent<StoreObject>().RotationPoint.rotation = _displayObject.GetComponent<StoreObject>().RotationPoint.rotation;
        newObject.GetComponent<StoreObject>().AssignSO(_assignedObject);
        _surface.BuildNavMesh();
        if (!_placeMultiple)
        {
            _picker.CancelSelection();
        }

        if (!_tutorial) return;
        ObjectPlaced?.Invoke();
        ObjectPlaced -= _tutorial.ChangeSequenceIndex;
    }
}
