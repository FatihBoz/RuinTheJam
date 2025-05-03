using System;
using System.Collections;
using UnityEngine;

public class PlayerCombat : Player, IDamageReceiver
{
    public static Action OnPlayerDied;
    [SerializeField] private Animator animator;
    [SerializeField] private Weapon weapon;
    [SerializeField] private Transform heartsUi;
    private bool attackLoop = false;
    private int maxHearts = 3;
    private int currentHearts;

   

    private void Start()
    {
        currentHearts = maxHearts;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        inputActions.Player.Attack.performed += ctx => Attack();

    }

    private void Attack()
    {
        if (!attackLoop)
        {
            attackLoop = true;
            StartCoroutine(AttackLoop());
            
        }
        PlaySwordAnimation();
    }

    void PlaySwordAnimation()
    {
        if (weapon.canAttacksCombined && !weapon.biggerAttackIsReady)
        {
            weapon.MakeWeaponBigger();
        }
        else
        {
            weapon.AnimationStart();
            animator.SetTrigger(AnimationKey.PlayerSwordAttack);
        }
        
    }

    public void ReceiveDamage(float damageAmount)
    {
        --currentHearts;
        heartsUi.GetChild(currentHearts).gameObject.SetActive(false);
        if (currentHearts <= 0)
        {
            OnPlayerDied?.Invoke();
            Destroy(gameObject);
        }
    }


    private IEnumerator AttackLoop()
    {
        while (attackLoop)
        {
            yield return new WaitForSeconds(2f);
            PlaySwordAnimation();
        }
    }
}
