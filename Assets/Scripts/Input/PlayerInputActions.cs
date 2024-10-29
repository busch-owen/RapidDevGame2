using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInputActions : MonoBehaviour
{
    private PlayerInputControls playerInput;
    public PlayerMovement pM;// MovementScript\
    private EmployeeStateMachine employeeStateMachine;

    // Start is called before the first frame update
    void OnEnable()
    {

        if (playerInput == null)
        {
            employeeStateMachine = FindFirstObjectByType<EmployeeStateMachine>();
            playerInput = new PlayerInputControls();
            playerInput.Actions.Move.performed += i => pM.Move(i.ReadValue<Vector2>());
            playerInput.Actions.Stock.performed += i => employeeStateMachine.EnableStocking();
            playerInput.Actions.Not.performed += i => employeeStateMachine.DisableStocking();

        }

        playerInput.Enable();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
