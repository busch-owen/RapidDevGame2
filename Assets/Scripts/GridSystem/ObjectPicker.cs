using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    private ObjectPlacer _placer;

    private GameObject previousObject;

    private void Start()
    {
        _placer = FindFirstObjectByType<ObjectPlacer>();
    }

    public void PickSpecificObject(StoreObjectSO obj)
    {
        CancelSelection();
        previousObject = Instantiate(obj.PlaceModeObject);
        _placer.AssignObject(obj, previousObject);
        Debug.Log(obj);
        _placer.PlaceMultiple(true);
    }
    

    public void CancelSelection()
    {
        if (!previousObject) return;
        Destroy(previousObject.gameObject);
        _placer.ChangePlaceMode(false);
    }
}