using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeleter : MonoBehaviour
{
    public bool DeleteModeEnabled { get; set; }
    
    private MousePositionHandler _positionHandler;

    private ObjectMover _objectMover;

    private void Start()
    {
        _positionHandler = GetComponent<MousePositionHandler>();
        _objectMover = FindFirstObjectByType<ObjectMover>();
    }

    public void CheckForObjectToDelete()
    {
        if (_positionHandler.CheckForObjectDelete() == null || !DeleteModeEnabled) return;
        var tempObject = _positionHandler.CheckForObjectDelete();
        Destroy(tempObject.gameObject);
    }

    public void ToggleDeleteMode()
    {
        DeleteModeEnabled = !DeleteModeEnabled;
    }
}
