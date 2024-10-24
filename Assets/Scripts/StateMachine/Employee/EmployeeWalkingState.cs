using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeWalkingState : EmployeeBaseState
{
    public EmployeeWalkingState(EmployeeStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.IsWalking = true;
    }
}
