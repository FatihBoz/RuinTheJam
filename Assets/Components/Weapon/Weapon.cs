using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private LayerMask damageableLayer;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private Transform attackPos;

    private Collider2D enemyCollider;
    private bool effectCanBeInstantiated = true;
    private Vector3 originalScale;
    private float currentDamage;
    private float currentAttackRange = 0f;

    public bool isAttacking = false;
    public bool canAttacksCombined = false;
    public bool biggerAttackIsReady = false;


    private void Start()
    {
        currentDamage = damage;
        originalScale = transform.localScale;
        currentAttackRange = attackRange;
    }

    public void LoopAnimationStart()
    {
        canAttacksCombined = true;
    }

    public void MakeWeaponBigger()
    {
        currentAttackRange *= 3f;
        transform.localScale *= 3f;
        currentDamage *= 3f;
        biggerAttackIsReady = true;
    }

    public void OnAttackStarted()
    {
        PerformAttack();
        isAttacking = true;
    }

    public void OnAttackEnded()
    {
        currentAttackRange = attackRange;
        transform.localScale = originalScale;
        currentDamage = damage;
        biggerAttackIsReady = false;
    }

    public void OnBloodSplashed()
    {
        canAttacksCombined = false;

        if (!effectCanBeInstantiated ||enemyCollider == null) return;
        var effect = Instantiate(hitEffect, enemyCollider.ClosestPoint(transform.position), Quaternion.identity);
        if (enemyCollider.transform.rotation.y > 0)
        {
            effect.transform.localScale = new Vector3(-effect.transform.localScale.x, effect.transform.localScale.y, effect.transform.localScale.z);
        }
        isAttacking = false;
        Destroy(effect, 1f);

    }


    public void PerformAttack()
    {
        effectCanBeInstantiated = false;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPos.position, currentAttackRange, damageableLayer);
        foreach (var collider in hitColliders)
        {
            if (collider.TryGetComponent<IDamageReceiver>(out IDamageReceiver damageReceiver))
            {
                effectCanBeInstantiated = true;
                enemyCollider = collider;
                SoundManager.Instance.PlayHitClick();
                CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
                damageReceiver.ReceiveDamage(currentDamage);
 
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, currentAttackRange);
    }


}
