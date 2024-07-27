using TMPro;
using UnityEngine;

public class MaxPoint : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _maxPointValue;

    private void Awake()
    {
        _maxPointValue.text = Points.GetPoints().ToString();
    }
}
