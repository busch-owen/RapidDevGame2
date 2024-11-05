using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInputActions : MonoBehaviour
{
    private PlayerInputControls playerInput;
    public PlayerMovement pM;// MovementScript\
    private EmployeeStateMachine employeeStateMachine;
    private ShelfUi _shelfUi;
    private InvGrid _invGrid;

    // Start is called before the first frame update
    void OnEnable()
    {

        if (playerInput == null)
        {
            employeeStateMachine = FindFirstObjectByType<EmployeeStateMachine>();
            _shelfUi = FindFirstObjectByType<ShelfUi>();
            _invGrid = FindFirstObjectByType<InvGrid>();
            playerInput = new PlayerInputControls();
            playerInput.Actions.Move.performed += i => pM.Move(i.ReadValue<Vector2>());
            //playerInput.Actions.Stock.performed += i => employeeStateMachine.EnableStocking();
            //playerInput.Actions.Not.performed += i => employeeStateMachine.DisableStocking();
            playerInput.Actions.Esc.performed += i => _shelfUi.Show();
            playerInput.Actions.Esc.performed += i => _invGrid.DisableImage();
        }

        playerInput.Enable();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AssignUi(ShelfUi shelf)
    {
        _shelfUi = shelf;
    }
}
