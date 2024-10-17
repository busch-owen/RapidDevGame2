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
        ShelfCheck();
    }

    private void ShelfCheck()
    {
        var shelf = _stateMachine.Target.GetComponent<Shelf>();
        foreach (ItemTypeSo item in _stateMachine.Items)
        {
            if (shelf.AssignedItem != item) continue;
            _stateMachine.ItemsCollected.Add(item);
            Debug.Log(shelf.AssignedItem);
            _stateMachine.ChangeState(NpcStateName.PositiveDialog);
        }
        _stateMachine.ChangeState(NpcStateName.NegativeDialog);
    }
    
}
