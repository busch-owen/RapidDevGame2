using System;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    private StoreObjectSO _assignedObject;
    private GameObject _displayObject;
    public event Action ObjectPlaced;

    private ObjectPicker _picker;

    private GridPointer _pointer;

    private void Start()
    {
        _picker = FindFirstObjectByType<ObjectPicker>();
        _pointer = FindFirstObjectByType<GridPointer>();
    }

    public void AssignObject(StoreObjectSO obj, GameObject displayObject)
    {
        _assignedObject = obj;
        _displayObject = displayObject;
    }

    public void RotateObject()
    {
        var tempObj = _displayObject.GetComponent<StoreObject>();
        tempObj.RotationPoint.Rotate(Vector3.forward, 90f);
    }

    public void PlaceObject()
    {
        if(!_assignedObject || !_pointer.CursorOnGrid) return;
        var storeObject = _assignedObject.ObjectToPlace;
        var newObject = Instantiate(storeObject, _pointer.CurrentCellPos, Quaternion.identity);
        newObject.GetComponent<StoreObject>().RotationPoint.rotation = _displayObject.GetComponent<StoreObject>().RotationPoint.rotation;
        ObjectPlaced?.Invoke();
    }
}
