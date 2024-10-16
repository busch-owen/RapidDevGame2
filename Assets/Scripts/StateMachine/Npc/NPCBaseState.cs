using System.Collections;
using System.Collections.Generic;
using Code.Scripts.StateMachine;
using UnityEngine;
using UnityEngine.AI;

public class NPCBaseState : IState
{

    protected NpcStateMachine _stateMachine;

    public NPCBaseState(NpcStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    public virtual void Enter()
    {
        _stateMachine.ChangeState(this);
    }

    public virtual void Update()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Exit()
    {
        throw new System.NotImplementedException();
    }
}
