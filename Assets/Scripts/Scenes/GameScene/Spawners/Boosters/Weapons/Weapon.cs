using System.Linq;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    #region -Shoot-
    [Header ("Shoot")]
    [SerializeField] protected float _damage;
    [SerializeField] protected float _rate;
    public float Rate { get => _rate; }

    [SerializeField] protected Transform _firePoint;
    #endregion

    #region -Bullet-
    [Header ("Bullet")]
    [SerializeField] protected Bullet _bullet;
    [SerializeField] protected float _bulletForce = 20f;

    protected PoolBase<Bullet> _bulletsPool;
    #endregion

    private void Awake()
    {
        InitBulletsPool();
    }

    #region -PoolBullets-
    private void InitBulletsPool()
    {
        Transform rootPoolObject = FindAnyObjectByType<GameSceneManager>().BulletPoolObject.transform;
        Transform weaponPoolObject = rootPoolObject.Find(GetType().ToString());

        if (weaponPoolObject != null)
        {
            _bulletsPool = new(
                        delegate { return Preload(_bullet, weaponPoolObject); },
                        GetAction, ReturnAction, weaponPoolObject.GetComponentsInChildren<Bullet>(true).ToList(),
                        delegate { return Add(_bullet, ref _bulletsPool, weaponPoolObject); });
        }
        else
        {
            weaponPoolObject = new GameObject(GetType().ToString()).transform;
            weaponPoolObject.parent = rootPoolObject;

            _bulletsPool = new(
                        delegate { return Preload(_bullet, weaponPoolObject); },
                        GetAction, ReturnAction, Constants.Weapon.PRELOADBULLETCOUNT,
                        delegate { return Add(_bullet, ref _bulletsPool, weaponPoolObject); });
        }

        _bulletsPool.AttachLinkAll(InitBullets);
    }

    private Bullet Preload(Bullet prefab, Transform parentObject = null) => Instantiate(prefab, parentObject);

    private Bullet Add(Bullet prefab, ref PoolBase<Bullet> pool, Transform parentObject = null)
    {
        Bullet bullet = Instantiate(prefab, parentObject);
        bullet.Init(_damage, ref pool);
        return bullet;
    }
    private void GetAction(Bullet bullet) => bullet.gameObject.SetActive(true);
    private void ReturnAction(Bullet bullet) => bullet.gameObject.SetActive(false);

    private void InitBullets(Bullet bullet, PoolBase<Bullet> pool)
    {
        bullet.Init(_damage, ref pool);
    }
    #endregion

    public abstract void Shoot(Vector3 mousePosition);
}