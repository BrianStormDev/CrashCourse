using System.ComponentModel;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private DrivingInputHandler inputHandler;
    public Transform[] wheels; // Assigned for four wheel drive
    public Transform[] frontWheels; // Assigned for front wheel drive

    [Header("Car Settings")]
    [SerializeField] private float maxSpeed = 10f; // m/s
    [SerializeField] private float accelerationForce = 3000f;
    [SerializeField] private float brakeForce = 5000f;
    [SerializeField] private float maxSterringAngle = 30f;
    [SerializeField] private float wheelRadius = 0.35f; // Idk if this is necessary
    
    [Header("Car Data")]
    [ReadOnly] public float currentSpeed = 0f; // m/s

    
    LayerMask layerMask;


    void Awake()
    {
        layerMask = LayerMask.GetMask("Floor");
    }

    // All Physics related code should go in FixedUpdate
    void FixedUpdate()
    {
        RaycastHit hit;

        Transform[] transformArray = new Transform[4];

        foreach(Transform transform in transformArray)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);

            }
        } 
    }

    // Update handles movement
    void Update()
    {
        Vector3 move = new Vector3(inputHandler.movement.x, 0f, inputHandler.movement.y);
        move = move.normalized;
        transform.position += move * maxSpeed * Time.deltaTime;
    }
}
