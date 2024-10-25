using System.Collections;
using System.Collections.Generic;
using Code.Scripts.StateMachine;
using NUnit.Framework;
using UnityEngine;

public class NpcCheckoutState : NPCBaseState
{
    public NpcCheckoutState(NpcStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Checkout();
    }

    public override void FixedUpdate()
    {
        //GoToExit();
    }
    
    public bool ArrivedAtCheckout()
    {
        if (Vector2.Distance(_stateMachine.transform.position, _stateMachine.Agent.destination) <= 0.25f)
        {
            return true;
        }

        return false;
    }

    public void Checkout()
    {
        _stateMachine.RandomTarget = Random.Range(0, _stateMachine.Registers.Length);
        {
            _stateMachine.Target = _stateMachine.Registers[_stateMachine.RandomTarget].transform;
            _stateMachine.Agent.SetDestination(_stateMachine.Target.position);
        }
    }

    public void GoToExit()
    {
        if (ArrivedAtCheckout())
        {
            _stateMachine.MoneyManager.IncrementProfit(_stateMachine.MoneySpent);
            _stateMachine.MoneyManager.DecrementQuota(_stateMachine.MoneySpent);
            _stateMachine.ChangeState(NpcStateName.Exit);
        }
    }
}
