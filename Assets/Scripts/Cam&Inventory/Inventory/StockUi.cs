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

    [SerializeField] private Image image;

    public InvGrid grid;
    // Start is called before the first frame update
    void Start()
    {
        _eventManager = FindFirstObjectByType<EventManager>();
        _shelf = GetComponentInParent<Shelf>();
        grid = FindFirstObjectByType<InvGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked");
        _shelf = GetComponentInParent<Shelf>();
        if(_shelf.EmpStateMachine == null || _shelf.EmpStateMachine.currentState != _shelf.EmpStateMachine._employeeStockingState) return;
        if (_shelf.DistanceCheck())
        {
            _eventManager.InvokeShelfAssigned(_shelf);
            _shelf.SelectShelf();
            grid.EnableImage();
        }
        else
        {
            _shelf.DeselectShelf();
            grid.transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
