using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StockUi : MonoBehaviour, IPointerClickHandler
{

    public Shelf _shelf;

    private EventManager _eventManager;

    private PlayerInputActions _inputActions;

    [SerializeField] private Image image;

    public InvGrid grid;

    [SerializeField] private ShelfUi _ui;
    // Start is called before the first frame update
    void Start()
    {
        _eventManager = FindFirstObjectByType<EventManager>();
        _shelf = GetComponentInParent<Shelf>();
        grid = FindFirstObjectByType<InvGrid>();
        _inputActions = FindFirstObjectByType<PlayerInputActions>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked");
        _shelf = GetComponentInParent<Shelf>();
        if (_shelf.EmpStateMachine == null || _shelf.EmpStateMachine.currentState != _shelf.EmpStateMachine._employeeStockingState) return;
        
        if (_shelf.DistanceCheck())
        {
            _shelf.SelectShelf();
            Debug.Log($"assigned shelf: {_shelf.name}");
            grid.EnableImage(GetComponentInParent<Shelf>());
            _inputActions.AssignUi(_ui);
        }
        else
        {
            _shelf.DeselectShelf();
            grid.transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
