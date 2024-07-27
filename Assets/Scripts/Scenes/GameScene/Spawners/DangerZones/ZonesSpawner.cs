using System.Collections.Generic;
using UnityEngine;

public class ZonesSpawner : MonoBehaviour
{
    [Header("Border")]
    [SerializeField] private Area _border;
    [SerializeField] private float _distanceBetween = 3.0f;

    #region -Zones-
    [Header("Zones")]
    [SerializeField] private int _maxAttempts = 1000;
    [SerializeField] private List<ZoneObject> _zonesPrefab;

    private struct ZoneInLevel
    {
        public Vector2 position;
        public float radius;

        public ZoneInLevel(Vector2 position, float radius)
        {
            this.position = position;
            this.radius = radius;
        }
    }

    private List<ZoneInLevel> _zonesInLevel = new();
    #endregion

    private void Start()
    {
        GenerateZones();
    }

    private void GenerateZones()
    {
        int attempts = 0;

        float _xMax, _xMin, _yMax, _yMin;
        var (max, min) = _border.GetMaxMin();

        _xMax = max.x - _distanceBetween;
        _xMin = min.x + _distanceBetween;
        _yMax = max.y - _distanceBetween;
        _yMin = min.y + _distanceBetween;

        int countZones = 0;

        foreach (var zone in _zonesPrefab)
        {
            countZones += zone.count;

            while (_zonesInLevel.Count < countZones && attempts < _maxAttempts)
            {
                float radius = zone.GetRadius();

                Vector2 newZonePosition = new(
                    Random.Range(_xMin + radius, _xMax - radius),
                    Random.Range(_yMin + radius, _yMax - radius)
                );

                if (IsPositionValid(newZonePosition, radius))
                {
                    _zonesInLevel.Add(new(newZonePosition, radius));
                    Instantiate(zone.prefab, newZonePosition, Quaternion.identity, transform);
                }

                attempts++;
            }
        }
    }

    private bool IsPositionValid(Vector2 position, float radius)
    {
        foreach (var zone in _zonesInLevel)
        {
            if (Vector2.Distance(position, zone.position) < _distanceBetween + radius + zone.radius)
            {
                return false;
            }
        }
        return true;
    }
}

