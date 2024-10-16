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
            if (shelf.AssignedItem != item) continue;
            
            Debug.Log(shelf.AssignedItem);
            _stateMachine.TextIndex.StopAllCoroutines();
            _stateMachine.TextIndex.EnableText();
            _stateMachine.StartCoroutine(_stateMachine.TextIndex.TextVisible(_stateMachine.FoundText));
            _stateMachine.Invoke("ChooseTarget", 2.0f);
            _shelfChecked = false;

        }
        _stateMachine.TextIndex.StopAllCoroutines();
        _stateMachine.TextIndex.EnableText();
        _stateMachine.StartCoroutine(_stateMachine.TextIndex.TextVisible(_stateMachine.NotFoundText));
        _stateMachine.Invoke("ChooseTarget", 2.0f);
        _shelfChecked = false;
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
