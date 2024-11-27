using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnIneractAlternative;
    
    private PlayerInputActions playerInputActions;

    private void Awake(){
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Ineract_performed;
        playerInputActions.Player.interactAlternate.performed += Ineract_interactAlternate;
    }

    private void Ineract_interactAlternate(InputAction.CallbackContext context)
    {
        OnIneractAlternative?.Invoke(this,EventArgs.Empty);
    }

    private void Ineract_performed(InputAction.CallbackContext context)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized(){
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
