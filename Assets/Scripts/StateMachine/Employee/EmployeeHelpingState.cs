using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeHelpingState : EmployeeBaseState
{
    public EmployeeHelpingState(EmployeeStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public override void Enter()
    {
        if(_stateMachine.CurrentShelfs == null) return;

        foreach (var shelf in _stateMachine.CurrentShelfs)
        {
            shelf.ShelfClick.SwitchState(ShelfState.Helping);
        }
    }
}
