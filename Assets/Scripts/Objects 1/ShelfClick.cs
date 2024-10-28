using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShelfClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private NpcStateMachine _stateMachine;
    private Shelf _shelf;
    public Image image;
    public EmployeeStateMachine employeeStateMachine;
    private bool _hasBeenClicked;
    private EventManager _eventManager;
    private void Start()
    {
        _shelf = GetComponentInParent<Shelf>();
        _eventManager = FindFirstObjectByType<EventManager>();

    }

    private void Awake()
    {
        image = GetComponentInChildren<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _stateMachine = _shelf.StateMachine;
        if(!_stateMachine.CanTarget) return;
        if (_shelf.IsFlashing)
        {
            _stateMachine = _shelf.StateMachine;
            _stateMachine.CurrentShelf = _shelf;
            _stateMachine.Target = _shelf.transform;
            _eventManager.InvokeClicked();
            Debug.Log("disabled");
        }
        else
        {
            _stateMachine = _shelf.StateMachine;
            _stateMachine.CurrentShelf = _shelf;
            _stateMachine.Target = _shelf.transform;
            _stateMachine.Agent.SetDestination(_shelf.transform.position);
            _stateMachine.ChangeState(NpcStateName.Talking);
            image.enabled = false;
            _shelf.FlashColor();
            _hasBeenClicked = true;
            Debug.Log("Enabled");
        }
    }
}
