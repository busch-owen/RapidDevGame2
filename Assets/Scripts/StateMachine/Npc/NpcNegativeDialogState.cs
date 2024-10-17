using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcNegativeDialogState : NPCBaseState
{
    public NpcNegativeDialogState(NpcStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.TextIndex.StopAllCoroutines();
        _stateMachine.TextIndex.EnableText();
        _stateMachine.StartCoroutine(_stateMachine.TextIndex.TextVisible(_stateMachine.NotFoundText));
        if (_stateMachine.ItemsCollected.Count < _stateMachine.Items.Count)
        {
            _stateMachine.StartCoroutine(Switch());
        }
    }

    private IEnumerator Switch()
    {
        yield return new WaitForSeconds(2.0f);
        _stateMachine.ChangeState(NpcStateName.Wander);
        yield return null;
    }
    

}
