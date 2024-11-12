using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcKickedOutState : NPCBaseState
{
    public NpcKickedOutState(NpcStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.BeingKickedOut = true;
        _stateMachine.Agent.speed = 0.5f;
        _stateMachine.Agent.updatePosition = false;
        _stateMachine.Invoke("Struggle", 2.0f);
    }

    public override void FixedUpdate()
    {

        _stateMachine.transform.position = _stateMachine.EmployeeStateMachine.transform.position;
        if (Vector2.Distance(_stateMachine.transform.position, _stateMachine.Exit.position) <= 0.5f)
        {
            Debug.Log("E");
            _stateMachine.Die();
        }
    }
}
