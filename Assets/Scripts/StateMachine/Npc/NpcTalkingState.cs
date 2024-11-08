using System.Collections;
using System.Collections.Generic;
using Code.Scripts.StateMachine;
using UnityEngine;

public class NpcTalkingState : NPCBaseState
{
    public NpcTalkingState(NpcStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        if (_stateMachine.BeingKickedOut)
        {
            _stateMachine.ChangeState(NpcStateName.KickedOut);
        }
        // wander until found shelf with desired item on it
    }

    public override void FixedUpdate()
    {
        if (_stateMachine.ArrivedAtTarget())
        {
            _stateMachine.ChangeState(NpcStateName.CheckShelf);
        }
        //Debug.Log("wander");
    }

    
}
