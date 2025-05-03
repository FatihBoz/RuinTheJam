using UnityEngine;

public class EnemyWithGun : Enemy

{

    public EnemyGun enemyGun;

    public float attackPeriod = 1f;
    public override void Start()
    {
        base.Start();
        attackTimer=attackPeriod;
    }
    public override void AttackState()
    {
        if (playerDetected && playerTransform!=null)
        {
            
            LookToPlayer();
            enemyGun.Aim(playerTransform, facingDirection);
            if (attackTimer <= 0)
            {
                attackTimer = attackPeriod;
                enemyGun.Shoot();
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
}
