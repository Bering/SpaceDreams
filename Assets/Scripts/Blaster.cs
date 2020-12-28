using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField] BL_Bullet _bulletPrefab = null;
    [SerializeField] float reloadTime = 0.25f;
    
    ParticleSystem _muzzleFlash;
    float nextFireTime;

    void Awake()
    {
        _muzzleFlash = GetComponent<ParticleSystem>();
    }

    bool CanFire() {
        return Time.time >= nextFireTime;
    }

    public void Fire()
    {
        if (!CanFire()) return;

        nextFireTime = Time.time + reloadTime;

        var emission = _muzzleFlash.emission;
        emission.enabled = true;
        Instantiate(_bulletPrefab, transform.position, transform.rotation);
    }
}
