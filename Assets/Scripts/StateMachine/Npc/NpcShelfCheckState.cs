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
    }

    private void CheckShelf()
    {
        _stateMachine.ShelfCheck();
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
        else
        {
            _stateMachine.ChangeState(NpcStateName.NegativeDialog);
        }
    }
    
    
}
