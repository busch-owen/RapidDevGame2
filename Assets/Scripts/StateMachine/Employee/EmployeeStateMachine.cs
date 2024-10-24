using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.StateMachine;
using UnityEngine;
using UnityEngine.AI;

public enum EmployeeStates
{
    Enter,Idle,Exit,Stocking,Helping,CheckingOut,Ordering,Walking
}
public class EmployeeStateMachine : BaseStateMachine
{
    private EmployeeExitState _employeeExitState;
    private EmployeeHelpingState _employeeHelpingState;
    private EmployeeEnterState _employeeEnterState;
    private EmployeeOrderingState _employeeOrderingState;
    private EmployeeStockingState _employeeStockingState;
    private EmployeeIdleState _employeeIdleState;
    private EmployeeCheckingOutState _employeeCheckingOutState;
    private EmployeeWalkingState _employeeWalkingState;
    [field:SerializeField] public Transform Target{ get; set; }
    [field:SerializeField] public NavMeshAgent Agent{ get; set; }
    [field:SerializeField] public Shelf CurrentShelf{ get; set; }
    [field:SerializeField] public ItemTypeSo CurrentItem{ get; set; }
    [field:SerializeField] public int AmountOfItems{ get; set; }
    [field:SerializeField] public bool IsWalking { get; set; }
    [field:SerializeField] public NpcStateMachine npcStateMachine{ get; set; }

    private SpriteRenderer _renderer;

    public void Awake()
    {
        _employeeExitState = new EmployeeExitState(this);
        _employeeEnterState = new EmployeeEnterState(this);
        _employeeHelpingState = new EmployeeHelpingState(this);
        _employeeOrderingState = new EmployeeOrderingState(this);
        _employeeStockingState = new EmployeeStockingState(this);
        _employeeIdleState = new EmployeeIdleState(this);
        _employeeCheckingOutState = new EmployeeCheckingOutState(this);
        _employeeWalkingState = new EmployeeWalkingState(this);
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _renderer.transform.rotation = Quaternion.Euler(90,0,0);
        Agent.updateRotation = false;

    }

    public void ChangeState(EmployeeStates stateName)
    {
        switch (stateName)
        {
            case EmployeeStates.Enter:
                base.ChangeState(_employeeEnterState);
                break;
            case EmployeeStates.Exit:
                base.ChangeState(_employeeExitState);
                break;
            case EmployeeStates.Helping:
                base.ChangeState(_employeeHelpingState);
                break;
            case EmployeeStates.Ordering:
                base.ChangeState(_employeeOrderingState);
                break;
            case EmployeeStates.Stocking:
                base.ChangeState(_employeeStockingState);
                break;
            case EmployeeStates.Idle:
                base.ChangeState(_employeeIdleState);
                break;
            case EmployeeStates.CheckingOut:
                base.ChangeState(_employeeCheckingOutState);
                break;
            case EmployeeStates.Walking:
                base.ChangeState(_employeeWalkingState);
                break;
        }
    }

    public bool DistanceCheck()
    {
        if (Vector2.Distance(transform.position, Target.position) <= 0.25f)
        {
            return true;
        }
        return false;
    }

    public void SetNpcReference(NpcStateMachine stateMachine)
    {
        npcStateMachine = stateMachine;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(!IsWalking)return;
        if (other.GetComponent<NpcStateMachine>())
        {
            var Npc = other.GetComponent<NpcStateMachine>();
            npcStateMachine = Npc;
            npcStateMachine.CanTalk = true;
            Target = npcStateMachine.transform;
        }
    }

    public void SetDestination()
    {
        Debug.Log("dest");
        Agent.SetDestination(Target.position);
    }
}
