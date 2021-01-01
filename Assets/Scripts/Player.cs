using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class Player : NetworkBehaviour
{
    [Header("Flight model")]
    [SerializeField] float yawFactor = 10;
    [SerializeField] float thrustFactor = 10;
    [SerializeField] float rollFactor = 15;
    [SerializeField] float pitchFactor = 10;
    [SerializeField] float yawFriction = 0.9f;
    [SerializeField] float thrustFriction = 0.9f;
    [SerializeField] float rollFriction = 0.9f;
    [SerializeField] float pitchFriction = 0.9f;

    [Header("Input Actions")]
    [SerializeField] InputAction yawAction = null;
    [SerializeField] InputAction thrustAction = null;
    [SerializeField] InputAction rollAction = null;
    [SerializeField] InputAction pitchAction = null;
    [SerializeField] InputAction blastersAction = null;
    [SerializeField] InputAction missileAction = null;

    [Header("Debug info")]
    [SerializeField] float yaw = 0;
    [SerializeField] float thrust = 0;
    [SerializeField] float roll = 0;
    [SerializeField] float pitch = 0;
    [SerializeField] bool leftTrigger = false;
    [SerializeField] bool rightTrigger = false;
    [SerializeField] float yawRate = 0;
    [SerializeField] float thrustRate = 0;
    [SerializeField] float rollRate = 0;
    [SerializeField] float pitchRate = 0;

    Engine[] engines;
    Blaster[] blasters;
    AudioSource impactSound;

    void Awake()
    {
        engines = new Engine[2];
        engines[0] = transform.Find("Spaceship_Fighter/Engine-Left/Flame").GetComponent<Engine>();
        engines[1] = transform.Find("Spaceship_Fighter/Engine-Right/Flame").GetComponent<Engine>();

        blasters = new Blaster[2];
        blasters[0] = transform.Find("Spaceship_Fighter/Blaster-Left/Blaster").GetComponent<Blaster>();
        blasters[1] = transform.Find("Spaceship_Fighter/Blaster-Right/Blaster").GetComponent<Blaster>();

        impactSound = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        yawAction.Enable();
        thrustAction.Enable();
        rollAction.Enable();
        pitchAction.Enable();
        missileAction.Enable();
        blastersAction.Enable();
    }

    public override void OnStartAuthority()
    {
        transform.Find("Camera").gameObject.SetActive(true);
    }

    void Update()
    {
        if (!hasAuthority) return;

        yaw = yawAction.ReadValue<float>();
        thrust = thrustAction.ReadValue<float>();
        roll = rollAction.ReadValue<float>();
        pitch = pitchAction.ReadValue<float>();
        leftTrigger = (missileAction.ReadValue<float>() > 0.2f);
        rightTrigger = (blastersAction.ReadValue<float>() > 0.2f);

        rollRate = (rollRate + (-1 * roll * rollFactor * Time.deltaTime)) * rollFriction;
        pitchRate = (pitchRate + (pitch * pitchFactor * Time.deltaTime)) * pitchFriction;
        yawRate = (yawRate + (yaw * yawFactor * Time.deltaTime)) * yawFriction;

        transform.Rotate(pitchRate, yawRate, rollRate);

        thrustRate = (thrustRate + (thrust * thrustFactor * Time.deltaTime)) * thrustFriction;

        transform.position += transform.forward * thrustRate;

        CmdSetThrust(thrust);

        if (rightTrigger) {
            CmdFire();
        }
    }

    [Command]
    void CmdSetThrust(float thrust)
    {
        RpcSetThrust(thrust);
    }

    [ClientRpc]
    void RpcSetThrust(float thrust)
    {
        foreach(var engine in engines) {
            engine.SetThrust(thrust);
        }
    }

    [Command]
    void CmdFire()
    {
        RpcFire();
    }

    [ClientRpc]
    void RpcFire()
    {
        foreach(var blaster in blasters) {
            blaster.Fire();
        }
    }

    void OnGUI()
    {
        string s;

        GUI.BeginGroup(new Rect(Screen.width - 160, 0, 160, 140));
            GUI.Box(new Rect(0, 0, 160, 140), "");

            GUI.Label(new Rect(10, 10, 140, 20), "Yaw: " + yaw.ToString());
            GUI.Label(new Rect(10, 30, 140, 20), "Thrust: " + thrust.ToString());
            GUI.Label(new Rect(10, 50, 140, 20), "Roll: " + roll.ToString());
            GUI.Label(new Rect(10, 70, 140, 20), "Pitch: " + pitch.ToString());

            s = leftTrigger ? "pulled" : "released";
            GUI.Label(new Rect(10, 90, 140, 20), "Left Trigger: " + s);
            s = rightTrigger ? "pulled" : "released";
            GUI.Label(new Rect(10, 110, 140, 20), "Right Trigger: " + s);
        GUI.EndGroup();
    }

    public void Hit()
    {
        if (!hasAuthority) return;

        CmdHit();
    }

    [Command]
    void CmdHit()
    {
        RpcHit();
    }

    [ClientRpc]
    void RpcHit()
    {
        impactSound.Play();
    }
}
