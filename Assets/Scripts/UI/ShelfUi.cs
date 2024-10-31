using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfUi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0,0,0);
    }

    public void Show()
    {
        transform.localScale = new Vector3(0,0,0);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
