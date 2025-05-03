using UnityEngine;

public class PlayerCombat : Player, IDamageReceiver
{
    [SerializeField] private Animator animator;
    [SerializeField] private Weapon weapon;

    protected override void OnEnable()
    {
        base.OnEnable();
        inputActions.Player.Attack.performed += ctx => Attack();
        
    }

    private void Attack()
    {
        animator.SetTrigger(AnimationKey.PlayerSwordAttack);
        
    }



    public void ReceiveDamage(float damageAmount)
    {
        
    }
}
