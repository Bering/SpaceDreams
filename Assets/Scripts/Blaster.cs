using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField] BL_Bullet _bulletPrefab = null;
    [SerializeField] float reloadTime = 0.5f;
    
    float nextFireTime;

    public void Fire()
    {
        if (!CanFire()) return;

        nextFireTime = Time.time + reloadTime;

        Instantiate(_bulletPrefab, transform.position, transform.rotation);
    }

    bool CanFire() {
        return Time.time >= nextFireTime;
    }

}
