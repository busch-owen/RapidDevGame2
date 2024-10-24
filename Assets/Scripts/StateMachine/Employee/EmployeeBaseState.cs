using System.Collections;
using System.Collections.Generic;
using Code.Scripts.StateMachine;
using UnityEngine;

public class EmployeeBaseState : IState
{
    protected EmployeeStateMachine _stateMachine;

    public EmployeeBaseState(EmployeeStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    public virtual void Enter()
    {
        _stateMachine.ChangeState(this);
    }

    public virtual void Update()
    {
        
    }

    public virtual void Exit()
    {
        
    }

    public virtual void FixedUpdate()
    {
        
    }
}
