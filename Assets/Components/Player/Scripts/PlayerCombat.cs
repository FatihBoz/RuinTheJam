using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : Player, IDamageReceiver
{
    public static Action OnPlayerDied;
    [SerializeField] private Animator animator;
    [SerializeField] private Weapon weapon;
    [SerializeField] private Transform heartsUi;
    private bool attackLoop = false;
    private int maxHearts = 3;
    private int currentHearts;

    private WeaponType currentWeaponType;

    [SerializeField]
    private PlayerGun playerGun;

    [SerializeField]
    private Vector2 mousePosition;
    public void SwitchWeapon(WeaponType weaponType)
    {

        currentWeaponType = weaponType;
        switch (weaponType)
        {
            case WeaponType.GUN:
                playerGun.gameObject.SetActive(true);
                break;
            case WeaponType.SWORD:
                break;
            case WeaponType.SPEAR:
                break;
            default:
                break;
        }
    }
    private void Awake()
    {
        weapon = GetComponentInChildren<Weapon>();
    }


    private void Start()
    {
        currentHearts = maxHearts;
        currentWeaponType = WeaponType.SWORD;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        inputActions.Player.Attack.performed += ctx => Attack();
        inputActions.Player.MousePosition.performed += ctx => GetMousePosition(ctx);
        inputActions.Player.LevelReset.performed += ctx => OnPlayerDied?.Invoke();
        OnPlayerDied += OnPlayerDied_Combat;

    }
    private void GetMousePosition(InputAction.CallbackContext ctx)
    {
        mousePosition = ctx.ReadValue<Vector2>();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        inputActions.Player.Attack.performed -= ctx => Attack();
        inputActions.Player.MousePosition.performed -= ctx => GetMousePosition(ctx);
        inputActions.Player.LevelReset.performed -= ctx => OnPlayerDied?.Invoke();
        OnPlayerDied -= OnPlayerDied_Combat;
    }
    private void Update()
    {
        if (currentWeaponType == WeaponType.GUN)
        {
            playerGun.Aim(mousePosition);
        }

        
    }

    private void Attack()
    {
        switch (currentWeaponType)
        {
            case WeaponType.GUN:
                playerGun.Shoot();
                break;
            case WeaponType.SWORD:
                if (!attackLoop)
                {
                    swordIcon.gameObject.SetActive(true);
                    swordIcon.SetCooldown();
                    attackLoop = true;
                    StartCoroutine(AttackLoop());
                }
                PlaySwordAnimation();
                break;
            case WeaponType.SPEAR:

                break;
            default:
                break;
        }   
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
        }
    }

    private void OnPlayerDied_Combat()
    {
        Destroy(gameObject);
    }


    private IEnumerator AttackLoop()
    {
        while (attackLoop)
        {
            yield return new WaitForSeconds(swordIcon.FillTime);
            PlaySwordAnimation();
            swordIcon.gameObject.SetActive(true);
            swordIcon.SetCooldown();
        }
    }
}
