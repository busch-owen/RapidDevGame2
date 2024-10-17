using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    private ObjectPlacer _placer;

    private GameObject previousObject;

    private void Start()
    {
        _placer = FindFirstObjectByType<ObjectPlacer>();
    }

    public void PickSpecificObject(ObjectData obj)
    {
        previousObject = Instantiate(obj.PlaceModeObject);
    }
}