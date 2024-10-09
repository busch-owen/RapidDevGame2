using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;


public class NavGoal : MonoBehaviour
{
    public NavMeshAgent Agent;

    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        if (target == null)
        {
            
        }
        else
        {
            Agent.SetDestination(target.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
