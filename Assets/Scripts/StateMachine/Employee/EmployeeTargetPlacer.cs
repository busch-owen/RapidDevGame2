using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
public class EmployeeTargetPlacer : MonoBehaviour
{
    public NpcObstacle Obstacle;
    public EmployeeStateMachine employeeStateMachine;
    public bool Exists;
    private List<NpcObstacle> _obstacles = new();
    private NpcObstacle Current;
    void Start()
    {
        employeeStateMachine = GetComponent<EmployeeStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!employeeStateMachine.IsWalking)return;
        if (TutorialHandler.InDialogue) return;
        if (Input.GetMouseButtonDown(0) && !Exists)
        {
            Exists = true;
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            var ob = Instantiate(Obstacle, pos, Quaternion.identity);
            employeeStateMachine.Target = ob.transform;
            employeeStateMachine.SetDestination();
            Current = ob;
        }
        else if (Input.GetMouseButtonDown(0) && Exists)
        {
            Destroy(Current.gameObject);
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            var ob = Instantiate(Obstacle, pos, Quaternion.identity);
            employeeStateMachine.Target = ob.transform;
            employeeStateMachine.SetDestination();
            Current = ob;
        }
    }
    
}
