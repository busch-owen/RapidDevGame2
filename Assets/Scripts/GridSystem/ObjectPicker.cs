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

    public void PickSpecificObject(StoreObjectSO obj)
    {
        CancelSelection();

        previousObject = Instantiate(obj.PlaceModeObject);
        _placer.AssignObject(obj);
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