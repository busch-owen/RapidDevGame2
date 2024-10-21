using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    private ObjectPlacer _placer;

    private GameObject previousObject;

    private ObjectMover _objectMover;

    private ObjectDeleter _objectDeleter;

    private void Start()
    {
        _placer = FindFirstObjectByType<ObjectPlacer>();
        _objectDeleter = FindFirstObjectByType<ObjectDeleter>();
        _objectMover = FindFirstObjectByType<ObjectMover>();
    }

    public void PickSpecificObject(StoreObjectSO obj)
    {
        CancelSelection();
        Desstroy(previousObject);
        _objectMover.MoveModeEnabled = false;
        _objectDeleter.DeleteModeEnabled = false;
        if (obj != null)
        {
            var previous = Instantiate(obj.PlaceModeObject);
            previousObject = previous;
            _placer.AssignObject(obj, previous);
            _placer.PlaceMultiple(true);
            
        }
        
    }

    public void Desstroy(GameObject gameobject)
    {
        Destroy(gameobject);
    }

    public void CancelSelection()
    {
        if (!previousObject) return;
        Desstroy(previousObject);
        _placer.ChangePlaceMode(false);
    }
}