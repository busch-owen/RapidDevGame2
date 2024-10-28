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
    private MoneyManager _moneyManager;

    private void Start()
    {
        _positionHandler = GetComponent<MousePositionHandler>();
        _objectMover = GetComponent<ObjectMover>();
        _moneyManager = FindFirstObjectByType<MoneyManager>();
        _surface = FindFirstObjectByType<NavMeshSurface>();
    }

    public void CheckForObjectToDelete()
    {
        if (_positionHandler.CheckForObjectToInteract() == null || !DeleteModeEnabled) return;
        var tempObject = _positionHandler.CheckForObjectToInteract();
        _surface.BuildNavMesh();
        _moneyManager.IncrementProfit(tempObject.AssignedObject.ObjectPrice / 3);
        Destroy(tempObject.gameObject);
    }

    public void ToggleDeleteMode()
    {
        DeleteModeEnabled = !DeleteModeEnabled;
    }
}
