using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;

public class ObjectDeleter : MonoBehaviour
{
    public bool DeleteModeEnabled { get; set; }
    
    private MousePositionHandler _positionHandler;

    private ObjectMover _objectMover;

    private NavMeshSurface _surface;

    private void Start()
    {
        _positionHandler = GetComponent<MousePositionHandler>();
        _objectMover = FindFirstObjectByType<ObjectMover>();
        _surface = FindFirstObjectByType<NavMeshSurface>();
    }

    public void CheckForObjectToDelete()
    {
        if (_positionHandler.CheckForObjectToInteract() == null || !DeleteModeEnabled) return;
        var tempObject = _positionHandler.CheckForObjectToInteract();
        _surface.BuildNavMesh();
        Destroy(tempObject.gameObject);
    }

    public void ToggleDeleteMode()
    {
        DeleteModeEnabled = !DeleteModeEnabled;
    }
}
