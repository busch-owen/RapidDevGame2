using System;
using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    private ObjectPlacer _placer;

    private GameObject previousObject;

    private void Start()
    {
        _placer = FindFirstObjectByType<ObjectPlacer>();
        _placer.ObjectPlaced += RemovePreviousObject;
    }

    public void PickSpecificObject(GameObject obj)
    {
        CancelSelection();

        previousObject = Instantiate(obj);
        _placer.AssignObject(previousObject);
    }

    private void RemovePreviousObject()
    {
        previousObject = null;
    }

    public void CancelSelection()
    {
        if (previousObject)
        {
            Destroy(previousObject.gameObject);
        }
    }
}