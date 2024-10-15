using System;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    private GameObject _assignedObject;
    public event Action ObjectPlaced;

    private ObjectPicker _picker;

    private GridPointer _pointer;

    private void Start()
    {
        _picker = FindFirstObjectByType<ObjectPicker>();
        _pointer = FindFirstObjectByType<GridPointer>();
    }

    public void AssignObject(GameObject obj)
    {
        _assignedObject = obj;
    }
    
    public void PlaceObject()
    {
        if(!_assignedObject || !_pointer.CursorOnGrid) return;
        var storeObject = _assignedObject.GetComponent<StoreObject>();
        if(!storeObject.Placeable) return;
        storeObject.PlaceObject();
        ObjectPlaced?.Invoke();
        _picker.PickSpecificObject(_assignedObject);
        
    }
}
