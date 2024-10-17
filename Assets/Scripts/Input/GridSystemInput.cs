using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemInput : MonoBehaviour
{
    private GridInput _gridInput;

    private GridPointer _gridPointer;
    private ObjectPlacer _objectPlacer;
    private void OnEnable()
    {
        _gridPointer ??= FindFirstObjectByType<GridPointer>();
        _objectPlacer ??= FindFirstObjectByType<ObjectPlacer>();
        
        if (_gridInput != null) return;

        _gridInput = new GridInput();
        
        _gridInput.GridActions.MousePosition.performed += i => _gridPointer.ChangePointerPosition(i.ReadValue<Vector2>());
        _gridInput.GridActions.MouseClick.performed += i => _gridPointer.MouseClicked();
        //_gridInput.GridActions.Rotate.performed += i => _objectPlacer.RotateObject();
        
        SetActiveState(true);
    }

    private void OnDestroy()
    {
        SetActiveState(false);
    }

    public void SetActiveState(bool state)
    {
        switch (state)
        {
            case true:
            {
                _gridInput.Enable();
                break;
            }
            case false:
            {
                _gridInput.Disable();
                break;
            }
            
        }
    }
}
