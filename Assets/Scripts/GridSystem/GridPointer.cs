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
    
    [SerializeField] private GameObject gridPointer;
    
    public bool CursorOnGrid { get; private set; }
    
    public Vector3Int CurrentCellPos { get; private set; }

    private void Start()
    {
        _camera = Camera.main;
        _gridLayer = LayerMask.GetMask("Grid");
    }

    public void ChangePointerPosition(Vector2 position)
    {
        _mousePos = position;
        var hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(_mousePos), _camera.transform.forward, Mathf.Infinity ,_gridLayer);
        if (hit)
        {
            if (!gridPointer.activeSelf)
            {
                gridPointer.SetActive(true);
            }

            CursorOnGrid = true;
            CurrentCellPos = grid.WorldToCell(hit.point);
            gridPointer.transform.position = CurrentCellPos;
        }
        else
        {
            CursorOnGrid = false;
            if (gridPointer.activeSelf)
            {
                gridPointer.SetActive(false);
            }
        }
    }
}
