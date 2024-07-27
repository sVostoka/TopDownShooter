using TMPro;
using UnityEngine;

public class LoseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _newRecord;
    [SerializeField] private TextMeshProUGUI _sessionPoints;

    public void Show()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);

        _sessionPoints.text = GameSceneManager.s_Instance.Points.ToString();

        if (GameSceneManager.s_Instance.Points > Points.GetPoints())
        {
            Points.SetPoints(GameSceneManager.s_Instance.Points);
            _newRecord.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
