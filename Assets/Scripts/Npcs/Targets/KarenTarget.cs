using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class KarenTarget : MonoBehaviour
{

    [FormerlySerializedAs("navGoal")] public NPCAI npcai;
    // Start is called before the first frame update
    void Start()
    {
        npcai = GetComponent<NPCAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
