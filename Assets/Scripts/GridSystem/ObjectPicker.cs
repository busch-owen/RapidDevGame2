using System.Collections.Generic;
using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    private ObjectPlacer _placer;

    private GameObject previousObject;
    [field: SerializeField] public List<StoreObjectSO> AllObjects { get; private set; }

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
        Destroy(previousObject);
        _objectDeleter.DeleteModeEnabled = false;
        if (obj == null) return;
        var previous = Instantiate(obj.PlaceModeObject);
        previousObject = previous;
        _placer.AssignObject(obj, previous);
        _placer.PlaceMultiple(true);
    }

    public void CancelSelection()
    {
        if (!previousObject) return;
        Destroy(previousObject);
        _placer.ChangePlaceMode(false);
    }
}