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
    private void Start()
    {
        _shelf = GetComponentInParent<Shelf>();
        
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
        _stateMachine.CurrentShelf = _shelf;
        _stateMachine.Target = _shelf.transform;
        _stateMachine.Agent.SetDestination(_shelf.transform.position);
        _stateMachine.ChangeState(NpcStateName.Talking);
        image.enabled = false;
        _shelf.IsFlashing = false;
        _shelf.FlashColor();
    }
}
