using System.Collections;
using System.Collections.Generic;
using Code.Scripts.StateMachine;
using UnityEngine;

public class NpcPositiveDialogState : NPCBaseState
{
    public NpcPositiveDialogState(NpcStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.TextIndex.StopAllCoroutines();
        _stateMachine.TextIndex.EnableText();
        _stateMachine.StartCoroutine(_stateMachine.TextIndex.TextVisible(_stateMachine.FoundText));
        if (_stateMachine.ItemsCollected.Count < _stateMachine.Items.Count)
        {
            _stateMachine.StartCoroutine(Switch());
        }
        else
        {
            _stateMachine.StartCoroutine(SwitchToCheckout());
            Debug.Log("Checkout");
        }
        
        
    }
    
    private IEnumerator Switch()
    {
        yield return new WaitForSeconds(2.0f);
        _stateMachine.ChangeState(NpcStateName.Wander);
        yield return null;
    }
    
    private IEnumerator SwitchToCheckout()
    {
        yield return new WaitForSeconds(2.0f);
        _stateMachine.ChangeState(NpcStateName.Checkout);
        yield return null;
    }
}
