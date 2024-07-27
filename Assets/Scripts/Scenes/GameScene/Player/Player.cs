using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region -Move-
    [Header ("Move")]
    [SerializeField] private float _speed = 4f;
    public float Speed { get => _speed* SpeedCoefficient; }
    #endregion

    #region -Rotate-
    [Header ("Rotate")]
    [SerializeField] private float _rotationSpeed = 180f;
    public float RotationSpeed { get => _rotationSpeed * SpeedCoefficient;  }
    #endregion

    #region -Boosts-
    public float SpeedCoefficient { get; set; } = 1;
    public bool Immortality { get; set; } = false;
    #endregion

    #region -Shooting-
    public event Action WeaponChange;
    
    private Weapon _weapon;
    public Weapon Weapon 
    { 
        get => _weapon;
        set
        {
            _weapon = value;

            if(WeaponChange != null)
            {
                WeaponChange.Invoke();
            }
        }
    }
    #endregion
}
