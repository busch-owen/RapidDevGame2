using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeKickingOutState :  EmployeeBaseState
{

    public EmployeeKickingOutState(EmployeeStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public override void Enter()
    {
        _stateMachine.KickingOut = true;
        _stateMachine.ManualOverrideOn();
    }
    
}
