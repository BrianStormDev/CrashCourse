using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Transform FL_Wheel_Transform;
    [SerializeField] private Transform FR_Wheel_Transform;
    [SerializeField] private Transform RL_Wheel_Transform;
    [SerializeField] private Transform RR_Wheel_Transform;
    
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
        transformArray[0] = FL_Wheel_Transform;
        transformArray[1] = FR_Wheel_Transform;
        transformArray[2] = RL_Wheel_Transform;
        transformArray[3] = RR_Wheel_Transform;

        foreach(Transform transform in transformArray)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
        } 
    }
}
