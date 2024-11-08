using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public bool MoveModeEnabled { get; set; }
    
    private MousePositionHandler _positionHandler;
    private ObjectPicker _picker;
    private ObjectPlacer _placer;
    private ObjectDeleter _deleter;
    private TutorialHandler _tutorial;

    private void Start()
    {
        _positionHandler = GetComponent<MousePositionHandler>();
        _picker = FindFirstObjectByType<ObjectPicker>();
        _placer = FindFirstObjectByType<ObjectPlacer>();
        _deleter = FindAnyObjectByType<ObjectDeleter>();
        _tutorial = FindAnyObjectByType<TutorialHandler>();
    }

    public void CheckForObjectToMove()
    {
        if (_positionHandler.CheckForObjectToInteract() == null || !MoveModeEnabled || TutorialHandler.InDialogue) return;
        var tempObject = _positionHandler.CheckForObjectToInteract();
        _picker.PickSpecificObject(tempObject.AssignedObject);
        _placer.PlaceMultiple(false);
        _placer.ChangePlaceMode(true);
        Destroy(tempObject.gameObject);
    }

    public void ToggleMoveMode(bool newState)
    {
        _deleter.DeleteModeEnabled = false;
        MoveModeEnabled = newState;
        if (!_tutorial) return;
        _placer.ObjectPlaced += _tutorial.ChangeSequenceIndex;
        _tutorial = null;
    }

    public void EnableMoveMode()
    {
        MoveModeEnabled = true;
    }
}
