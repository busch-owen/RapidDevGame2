using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GridPointer : MonoBehaviour
{
    private Vector2 _mousePos;
    private Camera _camera;

    private LayerMask _gridLayer;

    [SerializeField] private Grid grid;

    [SerializeField] private GameObject debugPointer;
    [SerializeField] private GameObject gridPointer;

    private void Start()
    {
        _camera = Camera.main;
        _gridLayer = LayerMask.GetMask("Grid");
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(_mousePos), _camera.transform.forward, _gridLayer);
        if (hit)
        {
            debugPointer.transform.position = hit.point;
            var cellPos = grid.WorldToCell(hit.point);
            gridPointer.transform.position = cellPos;
        }
    }

    public void ChangePointerPosition(Vector2 position)
    {
        _mousePos = position;
    }
}
