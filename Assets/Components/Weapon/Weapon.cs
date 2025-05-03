using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private LayerMask damageableLayer;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private Transform attackPos;

    [Header("Screen Shake")]
    [SerializeField] private float shakeMagnitude = 0.05f;
    [SerializeField] private float shakeDuration = 0.1f;

    private Collider2D enemyCollider;
    private bool effectCanBeInstantiated = true;
    private Vector3 originalScale;
    private float currentAttackRange = 0f;

    public bool isAttacking = false;
    public bool canAttacksCombined = false;
    public bool biggerAttackIsReady = false;
    private bool canAttack;


    private void Start()
    {
        originalScale = transform.localScale;
        currentAttackRange = attackRange;
    }

    public void AnimationStart()
    {
        canAttacksCombined = true;
    }

    public void MakeWeaponBigger()
    {
        currentAttackRange *= 2f;
        transform.localScale *= 2;
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
                ScreenShake.Instance.Shake(0.05f, 0.1f);
                damageReceiver.ReceiveDamage(damage);
 
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, currentAttackRange);
    }


}
