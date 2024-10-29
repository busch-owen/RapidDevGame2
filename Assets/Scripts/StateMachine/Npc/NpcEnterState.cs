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

        foreach (var sprite in _stateMachine.PossibleOpening)
        {
            var random = Random.Range(0, _stateMachine.PossibleOpening.Count);
            _stateMachine.CurrentSprite = _stateMachine.PossibleOpening[random];
            _stateMachine.RandomizeImage(_stateMachine.CurrentSprite);
        }

        _stateMachine.Invoke(nameof(_stateMachine.ShowOpening), 0.1f);
        // wait for player or npc to interact for set amount of time
        // if no interaction after certain amount of time, exclaim about it and move to wander state
        _stateMachine.Invoke(nameof(_stateMachine.FirstWander), _stateMachine.TimeForFirstWander);
    }

    
}
