using UnityEngine;

public class WeaponBooster : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;

    public Weapon Weapon { get => _weapon; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            player.Weapon = _weapon;

            gameObject.SetActive(false);
        }
    }
}
