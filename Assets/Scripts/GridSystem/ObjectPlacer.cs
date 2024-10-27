using System;
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

    private void Start()
    {
        _picker = FindFirstObjectByType<ObjectPicker>();
        _pointer = FindFirstObjectByType<GridPointer>();
        _cursorOnUI = EventSystem.current.IsPointerOverGameObject();
        _surface = FindFirstObjectByType<NavMeshSurface>();
    }

    public void AssignObject(StoreObjectSO obj, GameObject displayObject)
    {
        _assignedObject = obj;
        _displayObject = displayObject;
        _placeModeEnabled = true;
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
        if(!_assignedObject || !_pointer.CursorOnGrid || _cursorOnUI || !_placeModeEnabled) return;
        var storeObject = _assignedObject.ObjectToPlace;
        if (_displayObject != null && !_displayObject.GetComponent<StoreObject>().Placeable)
        {
            return;
        }
        var newObject = Instantiate(storeObject, _pointer.CurrentCellPos, Quaternion.identity);
        newObject.GetComponent<StoreObject>().RotationPoint.rotation = _displayObject.GetComponent<StoreObject>().RotationPoint.rotation;
        newObject.GetComponent<StoreObject>().AssignSO(_assignedObject);
        _surface.BuildNavMesh();
        ObjectPlaced?.Invoke();
        if (!_placeMultiple)
        {
            _picker.CancelSelection();
        }
    }
}
