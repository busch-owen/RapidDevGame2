using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcNegativeDialogState : NPCBaseState
{
    public NpcNegativeDialogState(NpcStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        foreach (var sprite in _stateMachine.PossibleBad)
        {
            var random = Random.Range(0, _stateMachine.PossibleBad.Count);
            var RandSpite = _stateMachine.PossibleBad[random];
            _stateMachine.RandomizeImage(RandSpite);
        }

        _stateMachine.TimeForFirstWander = 1;

        _stateMachine.Invoke(nameof(_stateMachine.ChangeToNegative), 2.0f);
        _stateMachine.GiveUp();
        
        
        ChooseToLeave();
    }

    private IEnumerator Switch()
    {
        yield return new WaitForSeconds(3.0f);
        _stateMachine.ChangeState(NpcStateName.Wander);
        yield return null;
    }

    public void ChooseToLeave()
    {
        if (_stateMachine.ShelvesBeforeLeave <= 0)
        {
            _stateMachine.Reputation -= 0.1f;
            _stateMachine.ChangeState(NpcStateName.Exit);
        }
        else
        {
            _stateMachine.StartCoroutine(Switch());
        }
    }
    

}
