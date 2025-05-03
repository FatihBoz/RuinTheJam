using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Go(Vector2 direction, float speed)
    {
        rb.linearVelocity = direction * speed;
        Destroy(gameObject, 2f);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(transform.name + " hit " + collision.name);
        if (collision.TryGetComponent<IDamageReceiver>(out IDamageReceiver damageReceiver))
        {
            print(collision.name);
            damageReceiver.ReceiveDamage(1);
            
        }

        Destroy(gameObject);
    }

}
