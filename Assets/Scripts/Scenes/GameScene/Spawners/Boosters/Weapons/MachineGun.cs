using UnityEngine;

public class MachineGun : Weapon
{
    public override void Shoot(Vector3 mousePosition)
    {
        SimpleBullet bullet = _bulletsPool.GetElement().GetComponent<SimpleBullet>();
        bullet.transform.position = _firePoint.position;
        bullet.transform.rotation = _firePoint.rotation;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bullet.transform.up * _bulletForce, ForceMode2D.Impulse);
    }
}