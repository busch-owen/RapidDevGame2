using System;
using UnityEngine;
using UnityEngine.Serialization;

public class StoreObject : MonoBehaviour
{
    [field: SerializeField] public bool ObjectPlaced { get; private set; }
    [field: SerializeField] public Transform RotationPoint { get; private set; }

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

    private void Update()
    {
        _gridCursor = FindFirstObjectByType<ObjectPicker>()?.transform;
        
        if(ObjectPlaced) return;
        if (_gridCursor)
        {
            transform.position = _gridCursor.position;
        }
    }
}
    