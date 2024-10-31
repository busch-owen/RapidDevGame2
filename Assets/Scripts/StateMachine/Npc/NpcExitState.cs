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
        _stateMachine.Invoke("Leave", 2.0f);
        _stateMachine.SwipeTask.CheckingOut = false;
    }

    private void despawn()
    {
        if (ArrivedAtExit())
        {
            if (_stateMachine.Shoplifter)
            {
                _stateMachine.MoneyManager.DecrementProfit(_stateMachine.MoneySpent);
            }
            _stateMachine.Destroy();
        }
    }

    public bool ArrivedAtExit()
    {
        if (Vector2.Distance(_stateMachine.transform.position, _stateMachine.Exit.transform.position) <= 0.25f)
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
