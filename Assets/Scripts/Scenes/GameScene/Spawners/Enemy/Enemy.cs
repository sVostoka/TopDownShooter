using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _hp = 10;
    public float Hp 
    { 
        get => _hp; 
        private set 
        {
            _hp = value;

            if (_hp <= 0)
            {
                Death();
            }
        } 
    }

    [SerializeField] private float _speed = 3;
    [SerializeField] private int _point = 7;

    private Transform _playerPosition;
    private Rigidbody2D _rb;

    private PoolBase<GameObject> _pool;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerPosition = FindAnyObjectByType<Player>().transform;
    }

    private void FixedUpdate()
    {
        if(_playerPosition != null )
        {
            Vector2 direction = (_playerPosition.position - transform.position).normalized;
            _rb.MovePosition(_rb.position + direction * _speed * Time.fixedDeltaTime);

            _rb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player && !player.Immortality)
        {
            GameSceneManager.s_Instance.Lose();
        }
    }

    public void Init(ref PoolBase<GameObject> pool)
    {
        _pool = pool;
    }

    public void TakeDamage(float damage)
    {
        Hp -= damage;
    }

    private void Death()
    {
        _pool.Return(gameObject);
        GameSceneManager.s_Instance.Points += _point;
    }
}
