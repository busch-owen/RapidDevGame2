using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeIdleState : EmployeeBaseState
{
    public EmployeeIdleState(EmployeeStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.Agent.destination = _stateMachine.transform.position;
        _stateMachine.IsWalking = false;
    }
}
