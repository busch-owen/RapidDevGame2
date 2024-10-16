using System;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    private StoreObjectSO _assignedObject;
    public event Action ObjectPlaced;

    private ObjectPicker _picker;

    private GridPointer _pointer;

    private void Start()
    {
        _picker = FindFirstObjectByType<ObjectPicker>();
        _pointer = FindFirstObjectByType<GridPointer>();
    }

    public void AssignObject(StoreObjectSO obj)
    {
        _assignedObject = obj;
    }
    
    public void PlaceObject()
    {
        if(!_assignedObject || !_pointer.CursorOnGrid) return;
        var storeObject = _assignedObject.ObjectToPlace;
        Instantiate(storeObject, _pointer.CurrentCellPos, Quaternion.identity);
        ObjectPlaced?.Invoke();
    }
}
