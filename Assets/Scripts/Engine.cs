using UnityEngine;

public class Engine : MonoBehaviour
{
    ParticleSystem _flame;
    AudioSource _audio;

    void Awake()
    {
        _flame = GetComponent<ParticleSystem>();
        _audio = GetComponent<AudioSource>();
    }

    public void SetThrust(float thrust)
    {
        var main = _flame.main;
        main.startLifetime = Mathf.Lerp(0, 1, thrust);

        _audio.volume = Mathf.Lerp(0, 1, thrust);
    }
}
