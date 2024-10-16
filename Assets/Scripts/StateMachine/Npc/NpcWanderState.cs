using System.Collections;
using System.Collections.Generic;
using Code.Scripts.StateMachine;
using UnityEngine;

public class NpcWanderState : NPCBaseState
{
    public NpcWanderState(NpcStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void FixedUpdate()
    {
        _stateMachine.DistanceCheck();
        //Debug.Log("wander");
    }
}
