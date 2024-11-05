using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.StateMachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    public EmployeeStockingState _employeeStockingState;
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
    
    public EmployeeBaseState currentState{ get; private set; }

    [SerializeField] private string stateName;

    private SpriteRenderer _renderer;

    private ControlButton _controlButton;

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
        _controlButton = FindFirstObjectByType<ControlButton>();
        _controlButton.GetComponent<Button>().onClick.AddListener(ManualOverrideOn);
        _controlButton.GetComponent<Button>().onClick.AddListener(EnableStocking);
        _controlButton.ControlPanelCancelButton.onClick.AddListener(ManualOverrideOff);
        _controlButton.ControlPanelCancelButton.onClick.AddListener(DisableStocking);
        
    }
    
    public void ChangeState(EmployeeStates stateName)
    {
        switch (stateName)
        {
            case EmployeeStates.Enter:
                base.ChangeState(_employeeEnterState);
                currentState = _employeeEnterState;
                break;
            case EmployeeStates.Exit:
                base.ChangeState(_employeeExitState);
                currentState = _employeeExitState;
                break;
            case EmployeeStates.Helping:
                base.ChangeState(_employeeHelpingState);
                currentState = _employeeHelpingState;
                break;
            case EmployeeStates.Ordering:
                base.ChangeState(_employeeOrderingState);
                currentState = _employeeOrderingState;
                break;
            case EmployeeStates.Stocking:
                base.ChangeState(_employeeStockingState);
                currentState = _employeeStockingState;
                break;
            case EmployeeStates.Idle:
                base.ChangeState(_employeeIdleState);
                currentState = _employeeIdleState;
                break;
            case EmployeeStates.CheckingOut:
                base.ChangeState(_employeeCheckingOutState);
                currentState = _employeeCheckingOutState;
                break;
            case EmployeeStates.Walking:
                base.ChangeState(_employeeWalkingState);
                currentState = _employeeWalkingState;
                break;
        }

        this.stateName = currentState.ToString();
    }

    public bool DistanceCheck()
    {
        if (Vector2.Distance(transform.position, Target.position) <= 0.25f)
        {
            return true;
        }
        return false;
    }

    public void EnableStocking()
    {
        Debug.Log("stocking");
        ChangeState(EmployeeStates.Stocking);
    }

    public void DisableStocking()
    {
        Debug.Log("not");
        ChangeState(EmployeeStates.Idle);
    }

    public void SetNpcReference(NpcStateMachine stateMachine)
    {
        npcStateMachine = stateMachine;
    }

    public void ManualOverrideOn()
    {
        if (!IsWalking)
        {
            ChangeState(EmployeeStates.Walking);
            IsWalking = true;
        }
    }

    public void ManualOverrideOff()
    {
        if (IsWalking)
        {
            ChangeState(EmployeeStates.Idle);
        }
    }

    public void SetDestination()
    {
        if (!Agent) return;
        Agent?.SetDestination(Target.position);
    }
}
