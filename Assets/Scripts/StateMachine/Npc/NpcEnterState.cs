using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcEnterState : NPCBaseState
{
    public NpcEnterState(NpcStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.ChooseTarget();
        _stateMachine.ChangeState(NpcStateName.Wander);
        _stateMachine.StartText();
        
    }
    
    
}
