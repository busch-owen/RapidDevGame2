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
        _stateMachine.DistanceCheck();
    }

    public override void Enter()
    {
        if(_stateMachine.CurrentShelfs == null) return;
        foreach (var shelf in _stateMachine.CurrentShelfs)
        {
            Debug.Log(shelf);
            shelf.ShelfClick.SwitchState(ShelfState.Stocking);
        }
    }
}
