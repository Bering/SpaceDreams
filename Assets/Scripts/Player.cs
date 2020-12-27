using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float maxThrust = 1000;
    [SerializeField] float turnSpeed = 75;

    private Rigidbody body = null;

    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        body.AddTorque(transform.forward * -1 * Input.GetAxis("Horizontal") * turnSpeed);
        body.AddTorque(transform.right * Input.GetAxis("Vertical") * turnSpeed);

        body.AddTorque(transform.up * Input.GetAxis("Yaw") * turnSpeed);
        body.AddForce(transform.forward * Input.GetAxis("Thrust") * maxThrust);
    }
}
