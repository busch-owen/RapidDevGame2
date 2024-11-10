using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ShelfState
{
    Stocking,Helping
}

public class ShelfClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private NpcStateMachine _stateMachine;
    private Shelf _shelf;
    public Image image;
    private bool _hasBeenClicked;
    private EventManager _eventManager;
    public bool stockable;
    public EmployeeStateMachine EmpStateMachine;
    
    private PlayerInputActions _inputActions;
    public InvGrid grid;
    [SerializeField] private ShelfUi _ui;
    private void Start()
    {
        _shelf = GetComponentInParent<Shelf>();
        _eventManager = FindFirstObjectByType<EventManager>();
        grid = FindFirstObjectByType<InvGrid>();
        _inputActions = FindFirstObjectByType<PlayerInputActions>();
        _ui = FindFirstObjectByType<ShelfUi>();
        EmpStateMachine = FindFirstObjectByType<EmployeeStateMachine>();

    }

    private void Awake()
    {
        image = GetComponentInChildren<Image>();
        image.enabled = false;
    }

    public void SwitchState(ShelfState state)
    {
        switch (state)
        {
            case ShelfState.Stocking:
                stockable = true;
                break;
            case ShelfState.Helping:
                stockable = false;
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!_shelf.ObjectPlaced) return;
        if (stockable)
        {
            if (_shelf.DistanceCheck())
            {
                _shelf.SelectShelf();
                Debug.Log($"assigned shelf: {_shelf.name}");
                grid.EnableImage(_shelf);
                _inputActions.AssignUi(_ui);
            }
            else
            {
                _shelf.DeselectShelf();
                grid.transform.localScale = new Vector3(0, 0, 0);
            }
        }
        else
        {
            _stateMachine = _shelf.StateMachine;
            if(_stateMachine == null || !_stateMachine.CanTarget || _stateMachine.BeingKickedOut) return;
            if (!_hasBeenClicked)
            {
                _stateMachine = _shelf.StateMachine;
                _stateMachine.CurrentShelf = _shelf;
                _stateMachine.Target = _shelf.transform;
                _stateMachine.Agent.SetDestination(_shelf.transform.position);
                _stateMachine.ChangeState(NpcStateName.Talking);
                _shelf.FlashColor();
                _hasBeenClicked = true;
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
}
