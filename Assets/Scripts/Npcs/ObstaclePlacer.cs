using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ObstaclePlacer : MonoBehaviour
{
    // Start is called before the first frame update

    public NpcObstacle Obstacle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            var ob = Instantiate(Obstacle, pos, quaternion.identity);
        }
    }
}
