using UnityEngine;

public class SimpleBullet : Bullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            Damage(enemy);
        }

        ReturnInPool();
    }
}
