using UnityEngine;
using UnityEngine.InputSystem;

public class DrivingInputHandler : MonoBehaviour
{
    [HideInInspector] public Vector2 movement;

    // Here we can configure the inputs to be different based on the input handler
    // We want a keyboard handler and a microcontroller wheel handler
    
    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
}
