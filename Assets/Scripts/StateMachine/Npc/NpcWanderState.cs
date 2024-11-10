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
        _stateMachine.anim.clip = _stateMachine._animationClip;
        _stateMachine.ChooseTarget();
        // wander until found shelf with desired item on it
    }

    public override void FixedUpdate()
    {
        _stateMachine.DistanceCheck();
        //_stateMachine.anim.Play();
        //Debug.Log("wander");
    }
}
