using System;
using UnityEngine;
using UnityEngine.Serialization;

public class StoreObject : MonoBehaviour
{
    [field: SerializeField] public bool ObjectPlaced { get; private set; }
    [field: SerializeField] public Transform RotationPoint { get; private set; }
    public bool Placeable { get; private set; } = true;

    private Transform _gridCursor;

    [field: SerializeField]public SpriteRenderer Renderer { get; set; }

    [SerializeField] private BoxCollider2D objectCollider;
    
    public StoreObjectSO AssignedObject { get; private set; }

    public virtual void Start()
    {
        Renderer = GetComponentInChildren<SpriteRenderer>();
        
        objectCollider.isTrigger = ObjectPlaced switch
        {
            true => false,
            false => true
        };
    }

    private void Update()
    {
        _gridCursor = FindFirstObjectByType<ObjectPicker>()?.transform;
        
        if(ObjectPlaced) return;
        if (_gridCursor)
        {
            transform.position = _gridCursor.position;
        }
    }

    public void AssignSO(StoreObjectSO so)
    {
        AssignedObject = so;
    }
    
    public void OnTriggerStay2D(Collider2D other)
    {
        if (ObjectPlaced) return;
        Renderer.color = Color.red;
        Placeable = false;
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        if (ObjectPlaced) return;
        Renderer.color = Color.white;
        Placeable = true;
    }
}
    