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
        _stateMachine.ChangeTextPositive();
        if (_stateMachine.ItemsCollected.Count >= _stateMachine.Items.Count)
        {
            _stateMachine.StartCoroutine(SwitchToCheckout());
            Debug.Log("Checkout");
            
        }
        else if (_stateMachine.ItemsCollected.Count != 0 && _stateMachine.ShelvesBeforeLeave <= 0)
        {
            _stateMachine.StartCoroutine(SwitchToCheckout());
            Debug.Log("Checkout");
        }
        else
        {
            _stateMachine.StartCoroutine(Switch());
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
