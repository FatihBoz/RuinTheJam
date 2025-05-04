using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject hitEffectPrefab;
    private Rigidbody2D rb;
    public LayerMask hitLayer;
    public float damage = .5f;
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
                damageReceiver.ReceiveDamage(damage);
                var effect = Instantiate(hitEffectPrefab, collision.ClosestPoint(transform.position), Quaternion.identity);
                if (collision.transform.rotation.y > 0)
                {
                    effect.transform.localScale = new Vector3(-effect.transform.localScale.x, effect.transform.localScale.y, effect.transform.localScale.z);
                }
                SoundManager.Instance.PlayHitClick();
                Destroy(gameObject);

            }
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }


    }

}
