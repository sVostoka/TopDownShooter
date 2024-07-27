using System;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    private static GameSceneManager s_instance = null;
    public static GameSceneManager s_Instance { get => s_instance; }

    [SerializeField] private Canvas _ui;
    [SerializeField] private LoseMenu _loseMenu;

    [SerializeField] private GameObject _bulletPoolObject;
    public GameObject BulletPoolObject { get => _bulletPoolObject; }

    #region -Points-
    public event Action PointsChange;

    private int _points = 0;
    public int Points 
    { 
        get => _points;
        set
        {
            _points = value;

            if (PointsChange != null)
            {
                PointsChange.Invoke();
            }
        }
    }
    #endregion

    private void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
        else if (s_instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Lose()
    {
        _ui.gameObject.SetActive(false);
        _loseMenu.Show();
    }
}
