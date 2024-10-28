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
        _stateMachine.GiveUp();
        _stateMachine.ChangeToNegative();
        ChooseToLeave();
    }

    private IEnumerator Switch()
    {
        yield return new WaitForSeconds(2.0f);
        _stateMachine.ChangeState(NpcStateName.Wander);
        yield return null;
    }

    public void ChooseToLeave()
    {
        if (_stateMachine.ShelvesBeforeLeave <= 0)
        {
            _stateMachine.Reputation -= 0.1f;
            _stateMachine.ChangeState(NpcStateName.Exit);
        }
        else
        {
            _stateMachine.StartCoroutine(Switch());
        }
    }
    

}
