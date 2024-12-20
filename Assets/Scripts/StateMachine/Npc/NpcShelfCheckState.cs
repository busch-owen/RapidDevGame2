using System.Collections;
using System.Collections.Generic;
using Code.Scripts.StateMachine;
using UnityEngine;

public class NpcShelfCheckState : NPCBaseState
{
    public NpcShelfCheckState(NpcStateMachine stateMachine) : base(stateMachine)
    {
    }

    private bool _shelfChecked;

    public override void Enter()
    {
        CheckShelf();
        if (_stateMachine.EmployeeStateMachine == null)
        {
            _stateMachine.AssignEmployee();
        }
        _stateMachine.EmployeeStateMachine.ChangeState(EmployeeStates.Stocking);
    }

    private void CheckShelf()
    {
        _stateMachine.ShelfCheck();
        _stateMachine.Animator.SetTrigger("Searching");
        // check to see if item on shelf matches with budget
        // if the item is out of budget check for next item on list
        // if no items on list on shelf in budget move back to wander state
        
        //if out of shelves which match item type have percent chance to look for shelves with adjacent item types
    }

    public override void FixedUpdate()
    {
        ShowDialog();
    }

    public void ShowDialog()
    {
        if (_stateMachine.FoundItems)
        {
            _stateMachine.ChangeState(NpcStateName.PositiveDialog);
        }
    }

    public override void Exit()
    {
        _stateMachine.Animator.SetTrigger("Walking");
    }
}
