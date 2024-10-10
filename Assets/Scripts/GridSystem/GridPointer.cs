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
    
    public Vector3Int CurrentCellPos { get; private set; }

    private void Start()
    {
        _camera = Camera.main;
        _gridLayer = LayerMask.GetMask("Grid");
    }

    public void ChangePointerPosition(Vector2 position)
    {
        _mousePos = position;
        RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(_mousePos), _camera.transform.forward, _gridLayer);
        if (hit)
        {
            if (!gridPointer.activeSelf)
            {
                gridPointer.SetActive(true);
            }
            CurrentCellPos = grid.WorldToCell(hit.point);
            gridPointer.transform.position = CurrentCellPos;
        }
        else
        {
            if (gridPointer.activeSelf)
            {
                gridPointer.SetActive(false);
            }
        }
    }
}
