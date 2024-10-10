using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarenTarget : MonoBehaviour
{

    public NavGoal navGoal;
    // Start is called before the first frame update
    void Start()
    {
        navGoal = GetComponent<NavGoal>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
