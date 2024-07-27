using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grenade : Bullet
{
    [SerializeField] private float _radiusDamage = 2f;
    [SerializeField] private LayerMask _enemyMask;

    protected override void ReturnInPool()
    {
        List<Collider2D> enemies = Physics2D.OverlapCircleAll(transform.position, _radiusDamage, _enemyMask).ToList();

        foreach (var enemy in enemies)
        {
            Damage(enemy.gameObject.GetComponent<Enemy>());
        }

        base.ReturnInPool();
    }
}
