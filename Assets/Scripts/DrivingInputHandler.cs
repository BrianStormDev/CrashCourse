using UnityEngine;
using UnityEngine.InputSystem;

public class DrivingInputHandler : MonoBehaviour
{
    [HideInInspector] public Vector2 movement;
    
    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
}
