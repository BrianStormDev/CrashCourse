using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private DrivingInputHandler inputHandler;
    public Transform[] steerWheels; // What wheels are not driving
    public Transform[] driveWheels; // What wheels are driving
    [SerializeField] private Rigidbody rb;

    [Header("Car Settings")]
    [SerializeField] private float maxSpeed = 10f; // m/s
    [SerializeField] private float maxReversalSpeed = 10f; // m/s
    [SerializeField] private float accelerationForce = 10f;
    [SerializeField] private float brakeForce = 5f;
    [SerializeField] private float frictionForce = 5f;
    [SerializeField] private float maxSteeringAngle = 30f;
    [SerializeField] private float steerIncrement = 3f;
    [SerializeField] private float steeringForce = 30f;
    [SerializeField] private float wheelRadius = 0.35f; // Idk if this is necessary
    
    [Header("Car Data")]
    [ReadOnly] public float currentSpeed; // m/s
    [ReadOnly] public float currentAcceleration; // m/s^2
    [ReadOnly] public float throttleInput; // -1 to 1
    [ReadOnly] public float steerInput; // -1 to 1
    [ReadOnly] public float curSteerAngle; // -30deg to 30deg

    private Vector3 lastVelocity;

    
    LayerMask layerMask;


    void Awake()
    {
        layerMask = LayerMask.GetMask("Floor");
    }

    // All Physics related code should go in FixedUpdate
    void FixedUpdate()
    {
        // Read inputs from the handler
        throttleInput = inputHandler.movement.y;
        steerInput = inputHandler.movement.x;

        HandleAcceleration();
        HandleBraking();
        // HandleSteering();
        RotateWheels();

        // TODO: Add suspension

        UpdateStats();
    }

    void HandleAcceleration()
    {
        // I'm considering having some variable that controls how much torque is applied in the direction
        // We currently just have the accelerationForce generated as some static constant
        // But I can see that with each of the throttleInputs, a force is added
        Vector3 accelDir = transform.forward;
        float forwardSpeed = Vector3.Dot(rb.linearVelocity, accelDir);

        // Split force across wheels
        float forcePerWheel = accelerationForce / driveWheels.Length;

        foreach(Transform wheel in driveWheels)
        {   
            // Handle forward acceleration
            if (throttleInput > 0f)
            {
                rb.AddForce(accelDir * forcePerWheel * throttleInput, ForceMode.Acceleration);
            }
            // Handle reversing
            else if (throttleInput < 0)
            {
                rb.AddForce(accelDir * forcePerWheel * throttleInput, ForceMode.Acceleration);
            }
            
            // Clamp forward speed
            Vector3 localVel = transform.InverseTransformDirection(rb.linearVelocity);
            localVel.z = Mathf.Clamp(localVel.z, -maxReversalSpeed, maxSpeed);
            rb.linearVelocity = transform.TransformDirection(localVel);
        }
    }

    // void ClampForwardSpeed()
    // {
    //     Vector3 localVel = transform.InverseTransformDirection(rb.linearVelocity);
    //     localVel.z = Mathf.Clamp(localVel.z, -maxReversalSpeed, maxSpeed);
    //     rb.linearVelocity = transform.TransformDirection(localVel);
    // }

    void HandleBraking()
    {
        Vector3 localVel = transform.InverseTransformDirection(rb.linearVelocity);
        float zSpeed = localVel.z;

        foreach(Transform wheel in driveWheels)
        {   
            if (Mathf.Abs(zSpeed) > 0.01f)
            {
                // Apply braking opposite direction of movement
                Vector3 brakeDir = -transform.forward * Mathf.Sign(zSpeed);
                rb.AddForce(brakeDir * frictionForce, ForceMode.Acceleration);
            }
        }

        // Clamp to zero
        float stopThreshold = 0.2f;
        if (Mathf.Abs(zSpeed) < stopThreshold)
        {
            localVel.z = 0f;
            rb.linearVelocity = transform.TransformDirection(localVel);
        }
    }

    void HandleSteering() {
        // foreach(Transform wheel in steerWheels)
        // SteeringForce multiplier?
        // ForceMode.Impulse was the original version
        rb.AddTorque(Vector3.up * currentSpeed * curSteerAngle, ForceMode.Force);

        rb.AddTorque( Vector3.up * -rb.angularVelocity.y * steeringForce);

        // I want to be able to reduce the torque based on the current speed

        // Steering should be proportional to the speed 

    }

    // TODO: void HandleSuspension() {}

    void RotateWheels()
    {
        curSteerAngle += steerInput * steerIncrement;
        curSteerAngle = Mathf.Clamp(curSteerAngle, -maxSteeringAngle, maxSteeringAngle);

        foreach (Transform wheel in steerWheels)
        {
            wheel.localRotation = Quaternion.Euler(wheel.rotation.x, curSteerAngle, wheel.rotation.z);
        }

        // Note we also want to make a method that will rotate the wheels along their axes
    }

    void UpdateStats()
    {
        currentSpeed = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z).magnitude;
        currentAcceleration = (rb.linearVelocity - lastVelocity).magnitude / Time.fixedDeltaTime;
        lastVelocity = rb.linearVelocity;
    }


    // RaycastHit hit;

        // Transform[] transformArray = new Transform[4];

        // foreach(Transform transform in transformArray)
        // {
        //     if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        //     {
        //         Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        //     }
        //     else
        //     {
        //         Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);

        //     }
        // } 
}
