using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeleter : MonoBehaviour
{
    private bool _deleteModeEnabled;
    
    private MousePositionHandler _positionHandler;

    private void Start()
    {
        _positionHandler = GetComponent<MousePositionHandler>();
    }

    public void CheckForObjectToDelete()
    {
        if (_positionHandler.CheckForObject() == null || !_deleteModeEnabled) return;
        var tempObject = _positionHandler.CheckForObject();
        Destroy(tempObject.gameObject);
    }

    public void ToggleDeleteMode()
    {
        _deleteModeEnabled = !_deleteModeEnabled;
    }
}
