using System;
using UnityEngine;

public class MousePositionHandler : MonoBehaviour
{
    private LayerMask _objectLayer;
    private Camera _camera;

    private Vector3 _mousePos;

    private void Start()
    {
        _objectLayer = LayerMask.GetMask("Default");
        _camera = Camera.main;
    }

    public void GetCursorPosition(Vector2 pos)
    {
        _mousePos = pos;
    }
    
    public StoreObject CheckForObjectToInteract()
    {
        var hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(_mousePos), _camera.transform.forward, Mathf.Infinity, _objectLayer);
        if (!hit) return null;
        var objToInteract = hit.transform.GetComponent<StoreObject>();
        return objToInteract;
    }
}
