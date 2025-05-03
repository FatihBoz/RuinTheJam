using UnityEngine;

public class EnemyWithGun : Enemy

{

    public EnemyGun enemyGun;

  
    public override void AttackState()
    {
        if (playerDetected)
        {
            LookToPlayer();
            enemyGun.Aim(playerTransform, facingDirection);
            if (attackTimer <= 0)
            {
                attackTimer = 3f;
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
