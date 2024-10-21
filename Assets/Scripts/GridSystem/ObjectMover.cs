using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public bool MoveModeEnabled { get; set; }
    
    private MousePositionHandler _positionHandler;
    private ObjectPicker _picker;
    private ObjectPlacer _placer;
    private ObjectDeleter _deleter;

    private void Start()
    {
        _positionHandler = GetComponent<MousePositionHandler>();
        _picker = FindFirstObjectByType<ObjectPicker>();
        _placer = FindFirstObjectByType<ObjectPlacer>();
        _deleter = FindAnyObjectByType<ObjectDeleter>();
    }

    public void CheckForObjectToMove()
    {
        if (_positionHandler.CheckForObjectMove() == null || !MoveModeEnabled) return;
        var tempObject = _positionHandler.CheckForObjectMove();
        _picker.PickSpecificObject(tempObject.AssignedObject);
        _placer.PlaceMultiple(false);
        MoveModeEnabled = false;
        _picker.Desstroy(tempObject.gameObject);
    }

    public void ToggleMoveMode(bool newState)
    {
        _deleter.ToggleDeleteMode();
        MoveModeEnabled = newState;
    }
}
