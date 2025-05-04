
using Unity.VisualScripting;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerCombat>(out PlayerCombat player))
        {
            PlayerCombat.OnPlayerDied?.Invoke();
        }
        else if (collision.TryGetComponent<IDamageReceiver>(out var damageReceiver))
        {
            Destroy(collision.gameObject);
        }
    }
}
