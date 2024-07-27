using System.Collections;
using UnityEngine;

public class Speed : MonoBehaviour
{
    [SerializeField] private float _timeWork = 10f;
    [SerializeField] private float _speedBoost = 1.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            player.SpeedCoefficient *= _speedBoost;

            CoroutineManager.s_Instance.StartCoroutine(Deactivation(player));

            gameObject.SetActive(false);
        }
    }

    private IEnumerator Deactivation(Player player)
    {
        yield return new WaitForSeconds(_timeWork);
        player.SpeedCoefficient /= _speedBoost;
    }
}
