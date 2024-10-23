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
        _stateMachine.ShowOpening();
        // wait for player or npc to interact for set amount of time
        // if no interaction after certain amount of time, exclaim about it and move to wander state
        _stateMachine.Invoke(nameof(_stateMachine.FirstWander), _stateMachine.TimeForFirstWander);
    }

    
}
