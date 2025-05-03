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


    public void OnAttackStarted()
    {
        PerformAttack();
    }

    public void OnBloodSplashed()
    {
        if (!effectCanBeInstantiated) return;
        var effect = Instantiate(hitEffect, enemyCollider.ClosestPoint(transform.position), Quaternion.identity);
        if (enemyCollider.transform.rotation.y > 0)
        {
            effect.transform.localScale = new Vector3(-effect.transform.localScale.x, effect.transform.localScale.y, effect.transform.localScale.z);
        }
        Destroy(effect, 1f);
    }


    public void PerformAttack()
    {
        effectCanBeInstantiated = false;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, damageableLayer);
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


}
