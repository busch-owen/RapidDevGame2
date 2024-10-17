using System.Collections;
using System.Collections.Generic;
using Code.Scripts.StateMachine;
using UnityEngine;

public class NpcExitState : NPCBaseState
{
    public NpcExitState(NpcStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.Agent.SetDestination(_stateMachine.Exit.position);
    }

    private void despawn()
    {
        if (ArrivedAtExit())
        {
            _stateMachine.Destroy();
        }
    }

    public bool ArrivedAtExit()
    {
        if (Vector2.Distance(_stateMachine.transform.position, _stateMachine.Agent.destination) <= 0.25f)
        {
            return true;
        }

        return false;
    }

    public override void FixedUpdate()
    {
        despawn();
    }
}
