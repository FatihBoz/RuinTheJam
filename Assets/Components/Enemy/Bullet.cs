using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public LayerMask hitLayer;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Go(Vector2 direction, float speed)
    {
        rb.linearVelocity = direction * speed;
        Destroy(gameObject, 2f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hitLayer == (1 << collision.gameObject.layer))
        {
            if (collision.gameObject.TryGetComponent(out IDamageReceiver damageReceiver))
            {
                Debug.Log(collision.gameObject.name + "hasar aldi");
                damageReceiver.ReceiveDamage(1f);
            }
        }

        Destroy(gameObject);
    }

}
