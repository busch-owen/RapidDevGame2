using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeStockingState : EmployeeBaseState
{
    public EmployeeStockingState(EmployeeStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        _stateMachine.DistanceCheck();
    }
}
