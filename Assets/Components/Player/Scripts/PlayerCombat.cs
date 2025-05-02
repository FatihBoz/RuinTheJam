using UnityEngine;

public class PlayerCombat : Player
{
    private Animator animator;
    private bool canDamage = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        inputActions.Player.Attack.performed += ctx => Attack();
        
    }

    private void Attack()
    {
        animator.SetTrigger(AnimationKey.PlayerSwordAttack);
    }

    public void OnAttackStarted()
    {
        canDamage = true;
    }

    public void OnAttackFinished()
    {
        canDamage = false;
    }
}
