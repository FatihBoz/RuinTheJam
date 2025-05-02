using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Patrol,
        Attack
    }
    Rigidbody2D rb;

    bool isLedgeDetected = false;
    private bool playerDetected = false;

    public Transform ledgeTransform;
    public float ledgeDetectionDistance = 1f;
    int facingDirection = 1;
    public float speed = 2f;
    private EnemyState currentState;

    private float idleTime;


    public float playerDetectionDistance = 5f;
    public LayerMask playerLayer;
    private Transform playerTransform;

    public Transform gun;
    private float attackTimer = 3f;

    public Bullet bulletPrefab;
    public Transform bulletSpawnPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                IdleState();
                break;
            case EnemyState.Patrol:
                PatrolState();
                break;
            case EnemyState.Attack:
                AttackState();
                break;
            default:
                break;
        }
    }
    void FixedUpdate()
    {
        isLedgeDetected = !Physics2D.Raycast(ledgeTransform.position, Vector2.down, ledgeDetectionDistance, LayerMask.GetMask("Ground"));
        DetectEnemy();
        switch (currentState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Patrol:
                PatrolStateFixed();
                break;
            case EnemyState.Attack:
                break;
            default:
                break;
        }
    }
    private void AttackState()
    {
        if (playerDetected)
        {
            LookToPlayer();
            if (attackTimer<=0)
            {
                // attackTimer = 3f;
                Vector3 distance = playerTransform.position - gun.position;
                float accuracyError = Random.Range(-15f, 15f);
                float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg + accuracyError;
                if (facingDirection == -1)
                {
                    angle += 180;
                    angle *= -1;
                }
                gun.transform.localRotation = Quaternion.Euler(0, 0, angle);
                AttackTheGun(distance.normalized);

            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            idleTime = Random.Range(1f, 3f);
            currentState = EnemyState.Idle;
            return;
        }
    }
    private void AttackTheGun(Vector2 dir)
    {
        attackTimer = 3f;
        Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.Go(dir, 10f);
    }
    private void IdleState()
    {
        if (playerDetected)
        {
            currentState = EnemyState.Attack;    
            return;
        }
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        idleTime -= Time.deltaTime;
        if (idleTime<=0)
        {
            Flip();
            currentState = EnemyState.Patrol;
            return;
        }
    }
    private void DetectEnemy()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, playerDetectionDistance, playerLayer);
        playerDetected = player != null;
        if (playerDetected)
        {
            
            playerTransform = player.transform;
        }
    }
    private void PatrolState()
    {
        if (playerDetected)
        {
            currentState = EnemyState.Attack;
            return;
        }
    }

    private void PatrolStateFixed()
    {
        if (isLedgeDetected)
        {
            idleTime = Random.Range(1f, 3f);
            currentState = EnemyState.Idle;
            return;
        }

        rb.linearVelocity = new Vector2(speed * facingDirection, rb.linearVelocity.y);
    }

    private void LookToPlayer()
    {
        if (playerTransform.position.x - transform.position.x > 0)
        {
            if (facingDirection == -1)
            {
                Flip();
            }
        }
        else
        {
            if (facingDirection == 1)
            {
                Flip();
            }
        }
    }
    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingDirection *= -1;
    }

    private void OnDrawGizmos()
    {

        if (ledgeTransform != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(ledgeTransform.position, ledgeTransform.position+Vector3.down*ledgeDetectionDistance);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionDistance);
    }
}
