using UnityEngine;

public class EnemyWithSword : Enemy
{
    public Animator swordAnimator;

    public float attackRangeDistance;

  

    // Update is called once per frame
    public void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                IdleState();
                break;
            case EnemyState.Patrol:
                break;
            case EnemyState.Chase:
                break;
            case EnemyState.Attack:
                break;
            default:
                break;
        }
    }
    public void FixedUpdate()
    {
        PhysicChecks();
        DetectEnemy();
        switch (currentState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Patrol:
                PatrolState();
                break;
            case EnemyState.Chase:
                ChaseState();
                break;
            case EnemyState.Attack:
                break;
            default:
                break;
        }
    }
    protected override void PhysicChecks()
    {
        base.PhysicChecks();

    }
    private void IdleState()
    {
        if (playerDetected)
        {
            currentState=EnemyState.Chase;
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
    public void AttacKState()
    {
        
    }
    private void PatrolState()
    {
        if (isLedgeDetected || isWallDetected)
        {
            idleTime = Random.Range(1f, 3f);
            currentState = EnemyState.Idle;
            return;
        }
        rb.linearVelocity = new Vector2(speed * facingDirection, rb.linearVelocity.y);
        if (playerDetected)
        {
            currentState = EnemyState.Chase;
            return;
        }
    }

    private void ChaseState()
    {
        if (!playerDetected && playerTransform == null)
        {
            idleTime = Random.Range(1f, 3f);
            currentState = EnemyState.Idle;
            return;
        }
        if (Vector2.Distance(playerTransform.position,transform.position)<attackRangeDistance)
        {
            swordAnimator.SetTrigger(AnimationKey.PlayerSwordAttack);
            return;
        }
        LookToPlayer();
        rb.linearVelocity = new Vector2(1.5f*speed * facingDirection, rb.linearVelocity.y);
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRangeDistance);
    }
}
