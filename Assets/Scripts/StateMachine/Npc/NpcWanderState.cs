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
        _stateMachine.Animator.SetTrigger("Walking");
        _stateMachine.ChooseTarget();
        // wander until found shelf with desired item on it
    }

    public override void Exit()
    {
        
    }

    public override void FixedUpdate()
    {
        _stateMachine.DistanceCheck();
        //_stateMachine.anim.Play();
        //Debug.Log("wander");
    }
}
