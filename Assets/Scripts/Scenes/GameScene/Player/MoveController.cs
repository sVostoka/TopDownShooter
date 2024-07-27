using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Player _player;
    private Rigidbody2D _rb;

    private Vector2 _move;

    void Start()
    {
        _player = GetComponent<Player>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _move.x = Input.GetAxisRaw("Horizontal");
        _move.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _move * _player.Speed * Time.fixedDeltaTime);
    }
}
