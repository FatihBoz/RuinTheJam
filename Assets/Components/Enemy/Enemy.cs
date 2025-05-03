using UnityEngine;

public class Enemy : MonoBehaviour, IDamageReceiver
{
    public enum EnemyState
    {
        Idle,
        Patrol,
        Chase,
        Attack
    }
    protected Rigidbody2D rb;

    protected bool isLedgeDetected = false;
    protected bool isWallDetected = false;
    protected bool playerDetected = false;

    public Transform ledgeTransform;
    public float ledgeDetectionDistance = 1f;

    public Transform wallDetectTransform;
    public float wallDetectionDistance = 1f;


    protected int facingDirection = 1;
    public float speed = 2f;
    protected EnemyState currentState;

    protected float idleTime;


    public float playerDetectionDistance = 5f;
    public LayerMask playerLayer;
    protected Transform playerTransform;

    protected float attackTimer = 3f;



   
    public void Start()
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
        PhysicChecks();
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
   
    public virtual void AttackState()
    { }
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
    protected void DetectEnemy()
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
        if (isLedgeDetected || isWallDetected)
        {
            idleTime = Random.Range(1f, 3f);
            currentState = EnemyState.Idle;
            return;
        }

        rb.linearVelocity = new Vector2(speed * facingDirection, rb.linearVelocity.y);
    }

    protected void LookToPlayer()
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
    protected void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingDirection *= -1;
    }
    protected virtual void PhysicChecks()
    {
        isLedgeDetected = !Physics2D.Raycast(ledgeTransform.position, Vector2.down, ledgeDetectionDistance, LayerMask.GetMask("Ground"));
        isWallDetected = Physics2D.Raycast(wallDetectTransform.position, transform.right, wallDetectionDistance, LayerMask.GetMask("Ground"));
    }
    public virtual void OnDrawGizmos()
    {

        if (ledgeTransform != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(ledgeTransform.position, ledgeTransform.position+Vector3.down*ledgeDetectionDistance);
        }
        if (wallDetectTransform != null)
        {
            Gizmos.DrawLine(wallDetectTransform.position, wallDetectTransform.position + transform.right * wallDetectionDistance);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionDistance);
    }

    public void ReceiveDamage(float damageAmount)
    {
        
    }
}
