using UnityEngine;

public class GrenadeLauncher : Weapon
{
    public override void Shoot(Vector3 mousePosition)
    {
        Grenade grenade = _bulletsPool.GetElement().GetComponent<Grenade>();

        grenade.Range = Vector2.Distance(_firePoint.position, mousePosition);

        grenade.transform.position = _firePoint.position;
        grenade.transform.rotation = _firePoint.rotation;

        grenade.SetStartPosition();

        Rigidbody2D rb = grenade.GetComponent<Rigidbody2D>();
        rb.AddForce(grenade.transform.up * _bulletForce, ForceMode2D.Impulse);
    }
}
