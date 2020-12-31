using UnityEngine;
using Mirror;

public class Blaster : NetworkBehaviour
{
    [SerializeField] Bullet _bulletPrefab = null;
    [SerializeField] float _reloadTime = 0.5f;
    
    AudioSource _audio;
    float _nextFireTime;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void Fire()
    {
        if (!CanFire()) return;

        _nextFireTime = Time.time + _reloadTime;

        Bullet b = Instantiate(_bulletPrefab, transform.position, transform.rotation);
        NetworkServer.Spawn(b.gameObject);

        _audio.Play();
    }

    bool CanFire()
    {
        return Time.time >= _nextFireTime;
    }

}
