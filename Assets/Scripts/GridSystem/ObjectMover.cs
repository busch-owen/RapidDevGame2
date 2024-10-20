using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    private bool _moveModeEnabled;
    
    private MousePositionHandler _positionHandler;
    private ObjectPicker _picker;
    private ObjectPlacer _placer;

    private void Start()
    {
        _positionHandler = GetComponent<MousePositionHandler>();
        _picker = FindFirstObjectByType<ObjectPicker>();
        _placer = FindFirstObjectByType<ObjectPlacer>();
    }

    public void CheckForObjectToMove()
    {
        if (_positionHandler.CheckForObject() == null || !_moveModeEnabled) return;
        var tempObject = _positionHandler.CheckForObject();
        _picker.PickSpecificObject(tempObject.AssignedObject);
        _placer.PlaceMultiple(false);
        _moveModeEnabled = false;
        Destroy(tempObject.gameObject);
    }

    public void ToggleMoveMode(bool newState)
    {
        _moveModeEnabled = newState;
    }
}
