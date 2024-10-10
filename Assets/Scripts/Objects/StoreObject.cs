using System;
using UnityEngine;

public class StoreObject : MonoBehaviour
{
    public bool ObjectPlaced { get; private set; }

    private bool _placeable = true;

    private Transform _gridCursor;

    private SpriteRenderer _renderer;

    [SerializeField] private BoxCollider2D objectCollider;

    private void Start()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
        
        objectCollider.isTrigger = ObjectPlaced switch
        {
            true => false,
            false => true
        };
    }

    private void FixedUpdate()
    {
        _gridCursor = FindFirstObjectByType<ObjectPicker>()?.transform;
        
        if(ObjectPlaced) return;
        if (_gridCursor)
        {
            transform.position = _gridCursor.position;
        }
    }

    public void PlaceObject()
    {
        if (!_placeable) return;
        objectCollider.isTrigger = false;
        ObjectPlaced = true;
    }
    
    public void OnTriggerStay2D(Collider2D other)
    {
        if (ObjectPlaced) return;
        Debug.Log("intersecting " + other.name);
        _renderer.color = Color.red;
        _placeable = false;
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        if (ObjectPlaced) return;
        Debug.Log("no longer intersecting " + other.name);
        _renderer.color = Color.white;
        _placeable = true;
    }
}
    