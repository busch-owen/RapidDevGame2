using System.Collections;
using System.Collections.Generic;
using Code.Scripts.StateMachine;
using UnityEngine;

public class NpcPositiveDialogState : NPCBaseState
{
    public NpcPositiveDialogState(NpcStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.TextIndex.StopAllCoroutines();
        _stateMachine.TextIndex.EnableText();
        _stateMachine.StartCoroutine(_stateMachine.TextIndex.TextVisible(_stateMachine.FoundText));
        _stateMachine.Invoke("ChooseTarget", 2.0f);
    }
}
