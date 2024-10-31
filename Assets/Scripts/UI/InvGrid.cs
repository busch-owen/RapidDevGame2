using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvGrid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableImage()
    {
        transform.localScale = new Vector3(5.495245f,10.74701f,1);
    }
}
