using TMPro;
using UnityEngine;

public class GamePoints : MonoBehaviour
{
    private TextMeshProUGUI _points;

    private void Awake()
    {
        _points = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        SubscribePoints();
    }

    private void OnDestroy()
    {
        UnsubscribePoints();
    }

    private void SubscribePoints()
    {
        GameSceneManager.s_Instance.PointsChange += ChangePoints;
    }

    private void UnsubscribePoints()
    {
        GameSceneManager.s_Instance.PointsChange -= ChangePoints;
    }

    private void ChangePoints()
    {
        _points.text = GameSceneManager.s_Instance.Points.ToString();
    }
}
