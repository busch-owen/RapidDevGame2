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

    private void ShelfCheck()
    {
        var shelf = _stateMachine.Target.GetComponent<Shelf>();
        foreach (ItemTypeSo item in _stateMachine.Items)
        {
            if (shelf.AssignedItem != item)
            {
                Debug.Log(item);
            }
            else
            {
                Debug.Log(shelf.AssignedItem);
            }
        }
    }

    public override void FixedUpdate()
    {
        if (Vector2.Distance(_stateMachine.transform.position, _stateMachine.Agent.destination) <= 0.5f)
        {
            if(_shelfChecked) return;
            
            ShelfCheck();
            _shelfChecked = true;
        }
    }
}
