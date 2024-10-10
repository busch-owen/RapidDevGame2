using System;
using UnityEngine;
using UnityEngine.Serialization;

public class StoreObject : MonoBehaviour
{
    public bool ObjectPlaced { get; private set; }

    public bool Placeable { get; private set; } = true;

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
        if (!Placeable) return;
        objectCollider.isTrigger = false;
        ObjectPlaced = true;
    }
    
    public void OnTriggerStay2D(Collider2D other)
    {
        if (ObjectPlaced) return;
        _renderer.color = Color.red;
        Placeable = false;
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        if (ObjectPlaced) return;
        _renderer.color = Color.white;
        Placeable = true;
    }
}
    