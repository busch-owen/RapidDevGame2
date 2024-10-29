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
        if(_stateMachine == null || !_stateMachine.CanTarget) return;
        if (!_hasBeenClicked)
        {
            _stateMachine = _shelf.StateMachine;
            _stateMachine.CurrentShelf = _shelf;
            _stateMachine.Target = _shelf.transform;
            _stateMachine.Agent.SetDestination(_shelf.transform.position);
            _stateMachine.ChangeState(NpcStateName.Talking);
            _shelf.FlashColor();
            _hasBeenClicked = true;
            image.enabled = false;
            _eventManager.InvokeClicked();
        }
        else
        {
            _shelf = GetComponentInParent<Shelf>();
            _eventManager.InvokeClicked();
            _shelf.IsFlashing = false;
            _shelf.StopAllCoroutines();
            _hasBeenClicked = false;
        }
    }
}
