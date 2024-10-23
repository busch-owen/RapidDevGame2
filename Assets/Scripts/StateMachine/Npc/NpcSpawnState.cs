using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawnState : NPCBaseState
{
    public NpcSpawnState(NpcStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.Spawn();
        // wait for player or npc to interact for set amount of time
        // if no interaction after certain amount of time, exclaim about it and move to wander state

    }

    public override void FixedUpdate()
    {
        if (_stateMachine.ArrivedAtTarget())
        {
            _stateMachine.ChangeState(NpcStateName.Enter);
        }
    }
    
    
    
    
}
