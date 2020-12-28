using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float thrustFactor = 1000;
    [SerializeField] float rollFactor = 75;
    [SerializeField] float pitchFactor = 125;
    [SerializeField] float yawFactor = 75;

    private Rigidbody body = null;

    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        body.AddTorque(transform.forward * -1 * Input.GetAxis("Horizontal") * rollFactor);
        body.AddTorque(transform.right * Input.GetAxis("Vertical") * pitchFactor);

        body.AddTorque(transform.up * Input.GetAxis("Yaw") * yawFactor);
        body.AddForce(transform.forward * Input.GetAxis("Thrust") * thrustFactor);
    }
}
