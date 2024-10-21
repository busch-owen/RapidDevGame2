using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInputActions : MonoBehaviour
{
    private PlayerInputControls playerInput;
    public PlayerMovement pM;// MovementScript

    // Start is called before the first frame update
    void OnEnable()
    {

        if (playerInput == null)
        {
            playerInput = new PlayerInputControls();
            playerInput.Actions.Move.performed += i => pM.Move(i.ReadValue<Vector2>());

        }

        playerInput.Enable();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
