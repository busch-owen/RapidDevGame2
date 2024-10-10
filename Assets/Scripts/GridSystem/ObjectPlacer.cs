using System;
using UnityEngine;
public class ObjectPlacer : MonoBehaviour
{
    private GameObject _assignedObject;
    public event Action ObjectPlaced;

    public void AssignObject(GameObject obj)
    {
        _assignedObject = obj;
    }
    
    public void PlaceObject()
    {
        if(!_assignedObject) return;
        Debug.Log("placing object");
        _assignedObject.GetComponent<StoreObject>().PlaceObject();
        _assignedObject = null;
        ObjectPlaced?.Invoke();
    }
}
