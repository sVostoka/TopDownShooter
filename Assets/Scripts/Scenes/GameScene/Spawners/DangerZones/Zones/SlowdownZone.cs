using UnityEngine;

public class SlowdownZone : MonoBehaviour
{
    [SerializeField] private float _effectValue = 0.6f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            player.SpeedCoefficient *= _effectValue;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            player.SpeedCoefficient /= _effectValue;
        }
    }
}
