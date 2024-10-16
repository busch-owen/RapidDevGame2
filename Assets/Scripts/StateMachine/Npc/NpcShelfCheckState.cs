using System.Collections;
using System.Collections.Generic;
using Code.Scripts.StateMachine;
using UnityEngine;

public class NpcShelfCheckState : NPCBaseState
{
    public NpcShelfCheckState(NpcStateMachine stateMachine) : base(stateMachine)
    {
    }

    private void ShelfCheck()
    {

        var shelf = _stateMachine.Target.GetComponent<Shelf>();
        foreach (ItemTypeSo item in _stateMachine.Items)
        {
            if (shelf.AssignedItem != item)
            {
                Debug.Log(item);
            }
        }
    }
}
