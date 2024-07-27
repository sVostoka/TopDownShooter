using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            GameSceneManager.s_Instance.Lose();
        }
    }
}
