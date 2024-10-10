using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    [SerializeField] private Transform gridCursor;
    
    public void PickSpecificObject(GameObject obj)
    {
        var previousObject = gridCursor.GetComponentInChildren<StoreObject>();
        if (previousObject)
        {
            Destroy(previousObject.gameObject);
        }
        var temp = Instantiate(obj, gridCursor.transform.position, gridCursor.transform.rotation, gridCursor);
        var col = temp.GetComponentInChildren<BoxCollider2D>();
        col.isTrigger = true;
    }
}
