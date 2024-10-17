using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    private GridPointer _pointer;
    [SerializeField] private ObjectDatabaseSO database;
    private int _selectedObjectIndex = -1;

    [SerializeField] private GameObject gridVisualization;
    [SerializeField] private GameObject cellIndicator;

    private GridData objectsData;

    private Renderer previewRenderer;

    private List<GameObject> placedGameObjects = new();

    private void Start()
    {
        _pointer = FindFirstObjectByType<GridPointer>();
        objectsData = new();
    }
    
    public void StartPlacement(int ID)
    {
        StopPlacement();
        _selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if (_selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }
        gridVisualization.SetActive(true);
        cellIndicator.SetActive(true);
        _pointer.OnClicked += PlaceObject;
        _pointer.OnExit += Cancel;
    }
    
    private void StopPlacement()
    {
        _selectedObjectIndex = -1;
        gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);
        _pointer.OnClicked -= PlaceObject;
        _pointer.OnExit -= Cancel;
    }
    
    public void PlaceObject()
    {
        if (_pointer.IsPointerOverUI() || !_pointer.CursorOnGrid) return;

        var placementValidity = CheckPlacementValidity(_pointer.CurrentCellPos, _selectedObjectIndex);
        if (!placementValidity) return;
        var objToPlace = Instantiate(database.objectsData[_selectedObjectIndex].ObjectToPlace);
        objToPlace.transform.position = _pointer.CurrentCellPos;
        placedGameObjects.Add(objToPlace);
        objectsData.AddObjectsAt(_pointer.CurrentCellPos, 
            database.objectsData[_selectedObjectIndex].Size, 
            database.objectsData[_selectedObjectIndex].ID, 
            placedGameObjects.Count - 1);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        return objectsData.CanPlaceObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size);
    }

    public void Cancel()
    {
        
    }
}
