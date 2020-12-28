using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Flight model")]
    [SerializeField] float thrustFactor = 10;
    [SerializeField] float rollFactor = 15;
    [SerializeField] float pitchFactor = 10;
    [SerializeField] float yawFactor = 10;
    [SerializeField] float thrustFriction = 0.9f;
    [SerializeField] float rollFriction = 0.9f;
    [SerializeField] float pitchFriction = 0.9f;
    [SerializeField] float yawFriction = 0.9f;

    [Header("Debug info")]
    [SerializeField] float thrust = 0;
    [SerializeField] float roll = 0;
    [SerializeField] float pitch = 0;
    [SerializeField] float yaw = 0;
    [SerializeField] float thrustRate = 0;
    [SerializeField] float rollRate = 0;
    [SerializeField] float pitchRate = 0;
    [SerializeField] float yawRate = 0;

    ParticleSystem[] engineFlames;
    Blaster[] blasters;

    void Awake()
    {
        engineFlames = new ParticleSystem[2];
        engineFlames[0] = transform.Find("Spaceship_Fighter/Engine-Left/Flame").GetComponent<ParticleSystem>();
        engineFlames[1] = transform.Find("Spaceship_Fighter/Engine-Right/Flame").GetComponent<ParticleSystem>();

        blasters = new Blaster[2];
        blasters[0] = transform.Find("Spaceship_Fighter/Blaster-Left/Blaster").GetComponent<Blaster>();
        blasters[1] = transform.Find("Spaceship_Fighter/Blaster-Right/Blaster").GetComponent<Blaster>();
    }

    void Update()
    {
        thrust = Input.GetAxis("Thrust");
        roll = Input.GetAxis("Horizontal");
        pitch = Input.GetAxis("Vertical");
        yaw = Input.GetAxis("Yaw");

        rollRate = (rollRate + (-1 * roll * rollFactor * Time.deltaTime)) * rollFriction;
        pitchRate = (pitchRate + (pitch * pitchFactor * Time.deltaTime)) * pitchFriction;
        yawRate = (yawRate + (yaw * yawFactor * Time.deltaTime)) * yawFriction;

        transform.Rotate(pitchRate, yawRate, rollRate);

        thrustRate = (thrustRate + (Input.GetAxis("Thrust") * thrustFactor * Time.deltaTime)) * thrustFriction;

        transform.position += transform.forward * thrustRate;

        foreach(var ps in engineFlames) {
            var main = ps.main;
            main.startLifetime = Mathf.Lerp(0, 1, thrust);
        }

        if (Input.GetButton("Fire1")) {
            Fire();
        }
    }

    void Fire()
    {
        foreach(var blaster in blasters) {
            blaster.Fire();
        }
    }
}
