using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : Player, IDamageReceiver
{
    public static Action OnPlayerDied;
    public static Action OnLevelReset;
    [SerializeField] private Animator animator;
    [SerializeField] private Weapon weapon;
    [SerializeField] private Transform heartsUi;
    [SerializeField] private GameObject bloodExplosion;
    private bool attackLoop = false;
    private int maxHearts = 3;
    private int currentHearts;
    private bool previousAttackLoop = false;

    [SerializeField]
    private Weapon swordWeapon;
    [SerializeField]
    private Weapon spearWeapon;

    private WeaponType currentWeaponType;

    [SerializeField]
    private PlayerGun playerGun;

    [SerializeField]
    private Vector2 mousePosition;

    private DroppedWeapon triggeredDroppedWeapon;

    public void SwitchWeapon(WeaponType weaponType)
    {
        if (currentWeaponType==WeaponType.GUN)
        {
            playerGun.gameObject.SetActive(false);
        }
        else
        {
            weapon.gameObject.SetActive(false);
        }

        currentWeaponType = weaponType;
        
        switch (weaponType)
        {
            case WeaponType.GUN:
                playerGun.gameObject.SetActive(true);
                break;
            case WeaponType.SWORD:
                weapon = swordWeapon;
                weapon.gameObject.SetActive(true);
                break;
            case WeaponType.SPEAR:
                weapon = spearWeapon;
                weapon.gameObject.SetActive(true);
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
        inputActions.Player.Attack.performed += ctx => Attack(false);
        inputActions.Player.MousePosition.performed += ctx => GetMousePosition(ctx);
        inputActions.Player.LevelReset.performed += ctx => OnLevelReset?.Invoke();

    }
    private void GetMousePosition(InputAction.CallbackContext ctx)
    {
        mousePosition = ctx.ReadValue<Vector2>();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        inputActions.Player.Attack.performed -= ctx => Attack(false);
        inputActions.Player.MousePosition.performed -= ctx => GetMousePosition(ctx);
        inputActions.Player.LevelReset.performed -= ctx => OnLevelReset?.Invoke();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (triggeredDroppedWeapon!=null)
            {
                WeaponType weaponType = triggeredDroppedWeapon.weaponType;
                Destroy(triggeredDroppedWeapon.gameObject);
                SwitchWeapon(weaponType);

            }
        }
        if (currentWeaponType == WeaponType.GUN)
        {
            playerGun.Aim(mousePosition);
        }
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DroppedItem"))
        {
            triggeredDroppedWeapon = collision.GetComponent<DroppedWeapon>();
           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DroppedItem"))
        {
            if (triggeredDroppedWeapon != null && triggeredDroppedWeapon==collision.GetComponent<DroppedWeapon>())
            {
                triggeredDroppedWeapon = null;
            }
        }
    }
    private void Attack(bool loopAttack)
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
                PlaySwordAnimation(loopAttack);
                break;
            case WeaponType.SPEAR:

                break;
            default:
                break;
        }
        
    }

    void PlaySwordAnimation(bool loopAttack)
    {
        print("Loop Attack : " + loopAttack);
        print("previous loop attack : " + previousAttackLoop);

        if (weapon.canAttacksCombined && !weapon.biggerAttackIsReady && ((loopAttack && !previousAttackLoop) || (!loopAttack && previousAttackLoop)))
        {

            weapon.MakeWeaponBigger();
        }
        else
        {
            weapon.LoopAnimationStart();
            animator.SetTrigger(AnimationKey.PlayerSwordAttack);
        }
        previousAttackLoop = loopAttack;    
    }

    public void ReceiveDamage(float damageAmount)
    {
        --currentHearts;
        heartsUi.GetChild(currentHearts).gameObject.SetActive(false);
        if (currentHearts <= 0)
        {
            Destroy(Instantiate(bloodExplosion, transform.position, Quaternion.identity), 1f);
            OnPlayerDied?.Invoke();
            Destroy(gameObject);
        }
    }


    private IEnumerator AttackLoop()
    {
        while (attackLoop)
        {
            yield return new WaitForSeconds(swordIcon.FillTime);
            Attack(true);
            swordIcon.gameObject.SetActive(true);
            swordIcon.SetCooldown();
        }
    }
}
