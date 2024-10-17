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
        _stateMachine.ChangeTextNegative();
        _stateMachine.StartCoroutine(Switch());
    }

    private IEnumerator Switch()
    {
        yield return new WaitForSeconds(2.0f);
        _stateMachine.ChangeState(NpcStateName.Wander);
        yield return null;
    }
    

}
