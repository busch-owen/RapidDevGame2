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
        _stateMachine.Invoke("ChooseTarget", 2.0f);
    }
}
