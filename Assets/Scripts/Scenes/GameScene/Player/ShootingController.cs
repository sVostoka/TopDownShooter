using UnityEngine;

public class ShootingController : MonoBehaviour
{
    private Camera _camera;
    private Player _player;

    private Weapon _weapon;
    private float _nextTimeToFire = 0f;

    private Vector3 mousePosition;

    private float _targetZRotationPlayer;
    private float _targetZRotationWeapon;
    private bool _isRotating = false;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _camera = Camera.main;
    }

    private void Start()
    {
        SubscribeWeapon();

        _targetZRotationPlayer = transform.eulerAngles.z;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= _nextTimeToFire)
        {
            if (_weapon != null)
            {
                mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

                if (!_player.GetComponent<Collider2D>().bounds.Contains(new(mousePosition.x, mousePosition.y)))
                {
                    CalculateTargetRotation();

                    _isRotating = true;
                }
            }
        }

        if (_isRotating)
        {
            Rotate();
        }
    }

    private void OnDestroy()
    {
        UnsubscribeWeapon();
    }

    private void CalculateTargetRotation()
    {
        Vector2 directionPlayer = (mousePosition - transform.position).normalized;
        Vector2 directionWeapon = (mousePosition - _weapon.transform.position).normalized;

        _targetZRotationPlayer = Mathf.Atan2(directionPlayer.y, directionPlayer.x) * Mathf.Rad2Deg - 90;
        _targetZRotationWeapon = Mathf.Atan2(directionWeapon.y, directionWeapon.x) * Mathf.Rad2Deg - 90;
    }

    private void Rotate()
    {
        float currentZRotation = transform.eulerAngles.z;
        float newZRotation = Mathf.MoveTowardsAngle(currentZRotation, _targetZRotationPlayer, _player.RotationSpeed * Time.deltaTime);
        transform.eulerAngles = new(0, 0, newZRotation);

        if (Mathf.Abs(Mathf.DeltaAngle(currentZRotation, _targetZRotationPlayer)) <= 0.01)
        {
            _isRotating = false;

            _weapon.transform.eulerAngles = new(0, 0, _targetZRotationWeapon);

            _nextTimeToFire = Time.time + 1f / _weapon.Rate;
            _weapon.Shoot(mousePosition);
        }
    }

    private void SubscribeWeapon()
    {
        _player.WeaponChange += SwapWeapon;
    }

    private void UnsubscribeWeapon()
    {
        _player.WeaponChange += SwapWeapon;
    }

    private void SwapWeapon()
    {
        GameObject weapon = _player.GetComponentInChildren<Weapon>()?.gameObject;
        if(weapon != null) Destroy(weapon);

        _weapon = Instantiate(_player.Weapon, transform);
    }
}
