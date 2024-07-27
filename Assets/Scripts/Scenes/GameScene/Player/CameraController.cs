using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private Area _levelBorder;

    private Camera _camera;
    private float _xMax, _xMin, _yMax, _yMin;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Start()
    {
        CalculateCameraBounds();
    }

    void Update()
    {
        transform.position = new(
            Mathf.Clamp(_playerPosition.position.x, _xMin, _xMax),
            Mathf.Clamp(_playerPosition.position.y, _yMin, _yMax),
            transform.position.z);
    }

    private void CalculateCameraBounds()
    {
        float vExtends = _camera.orthographicSize;
        float hExtends = vExtends * _camera.aspect;
        
        var (max, min) = _levelBorder.GetMaxMin();

        _xMax = max.x - hExtends;
        _xMin = min.x + hExtends;
        _yMax = max.y - vExtends;
        _yMin = min.y + vExtends;
    }
}