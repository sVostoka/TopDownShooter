using System.Collections;
using UnityEngine;

public class Immortality : MonoBehaviour
{
    [SerializeField] private float _timeWork = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            player.Immortality = true;

            CoroutineManager.s_Instance.StartCoroutine(Deactivation(player));

            gameObject.SetActive(false);
        }
    }

    private IEnumerator Deactivation(Player player)
    {
        yield return new WaitForSeconds(_timeWork);
        player.Immortality = false;
    }
}
