using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 _input;
    public float speed = 5f;
    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        // Debug.Log(_input);
    }

    public void Update()
    {
        Vector3 movement = new Vector3(_input.x, 0f, _input.y) * speed * Time.deltaTime;
        transform.position += movement;
    }
}
