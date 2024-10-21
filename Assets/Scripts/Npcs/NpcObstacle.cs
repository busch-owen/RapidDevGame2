using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;


public class NpcObstacle : MonoBehaviour
{

    public NavMeshSurface Surface;
    // Start is called before the first frame update
    void Start()
    {
        Surface = FindFirstObjectByType<NavMeshSurface>();
        if(Surface == null ) return;
        Surface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
