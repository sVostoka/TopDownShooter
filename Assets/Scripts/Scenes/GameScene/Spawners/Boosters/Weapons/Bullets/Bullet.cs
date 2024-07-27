using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected PoolBase<Bullet> _pool;

    #region -Characteristics-
    [Header("Characteristics")]
    [SerializeField] protected float _range;
    protected float _damage;

    public float Range { get => _range; set => _range = value; }

    protected Vector2 _startPosition;
    #endregion

    public void Init(float damage, ref PoolBase<Bullet> pool)
    {
        _damage = damage;
        _pool = pool;
    }

    public void SetStartPosition()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (_range != float.PositiveInfinity && Vector2.Distance(_startPosition, transform.position) >= _range)
        {
            ReturnInPool();
        }
    }

    protected virtual void ReturnInPool()
    {
        _pool.Return(this);
    }

    protected void Damage(Enemy enemy)
    {
        enemy.TakeDamage(_damage);
    } 
}
