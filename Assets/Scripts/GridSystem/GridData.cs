using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridData
{
    private Dictionary<Vector3Int, PlacementData> placedObjects = new();

    public void AddObjectsAt(Vector3Int gridPosition, Vector2Int objectSize, int ID, int placedObjectIndex)
    {
        var postionToOccupy = CalculatePositions(gridPosition, objectSize);
        var data = new PlacementData(postionToOccupy, ID, placedObjectIndex);
        foreach (var pos in postionToOccupy)
        {
            if (placedObjects.ContainsKey(pos))
            {
                throw new Exception($"Dictionary already contains this cell position {pos}");
            }

            placedObjects[pos] = data;
        }
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> returnVal = new();
        for (var x = 0; x < objectSize.x; x++)
        {
            for (var y = 0; y < objectSize.y; y++)
            {
                returnVal.Add(gridPosition + new Vector3Int(x, 0, y));
            }
        }

        return returnVal;
    }
    
    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize)
    {
        var positionToOccupy = CalculatePositions(gridPosition, objectSize);
        return positionToOccupy.All(pos => !placedObjects.ContainsKey(pos));
    }
}

public class PlacementData
{
    public List<Vector3Int> OccupiedPositions;
    public int ID { get; private set; }
    public int PlacedObjectsIndex { get; private set; }

    public PlacementData(List<Vector3Int> occupiedPositions, int id, int placedObjectsIndex)
    {
        OccupiedPositions = occupiedPositions;
        ID = id;
        PlacedObjectsIndex = placedObjectsIndex;
    }
}
