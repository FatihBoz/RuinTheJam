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
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
