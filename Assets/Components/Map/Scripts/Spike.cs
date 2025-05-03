using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float knockbackForce = 50f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IDamageReceiver>(out IDamageReceiver damageReceiver))
        {
            damageReceiver.ReceiveDamage(1);
            collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb);
            {
                rb.linearVelocityY = 0;
                rb.AddForce((collision.transform.position - transform.position) * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}
