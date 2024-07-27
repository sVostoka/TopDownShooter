using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private float _spreadAngle = 10f;
    [SerializeField] private int _countBullet = 5;

    public override void Shoot(Vector3 mousePosition)
    {
        SimpleBullet bullet;

        float stepAngle = (_countBullet != 1) ? _spreadAngle / (_countBullet - 1) : _spreadAngle / 2;
        float startAngle = -_spreadAngle / 2;
        
        for (int i = 0; i < _countBullet; i++)
        {
            float currentAngle = startAngle + (stepAngle * i);
            Quaternion bulletRotation = Quaternion.Euler(new(0, 0, currentAngle));

            bullet = _bulletsPool.GetElement().GetComponent<SimpleBullet>();
            bullet.transform.position = _firePoint.position;
            bullet.transform.rotation = _firePoint.rotation * bulletRotation;

            bullet.SetStartPosition();

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(bullet.transform.up * _bulletForce, ForceMode2D.Impulse);
        } 
    }
}
