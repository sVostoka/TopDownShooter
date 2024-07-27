using UnityEngine;

public class Area : MonoBehaviour
{
    private RectTransform _border;

    private void Awake()
    {
        _border = GetComponent<RectTransform>();
    }

    public (Vector2 max, Vector2 min) GetMaxMin()
    {
        Vector3[] corners = new Vector3[4];
        _border.GetWorldCorners(corners);

        Vector2 max, min;

        max.x = corners[2].x;
        min.x = corners[0].x;
        max.y = corners[2].y;
        min.y = corners[0].y;

        return (max, min);
    }
}
