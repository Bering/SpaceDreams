using UnityEngine;

public class Freighter : MonoBehaviour
{
    [SerializeField] float speed = 1;

    Vector3 _movement;
    AudioSource _audio;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        _movement = transform.forward * speed * Time.deltaTime;
        transform.Translate(_movement);
    }

    public void Hit()
    {
        _audio.Play();
    }
}
