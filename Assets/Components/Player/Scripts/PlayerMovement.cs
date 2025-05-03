using System.Collections;
using UnityEngine;

public class PlayerMovement : Player
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckPoint;

    [Header("Dash")]
    [SerializeField] private GameObject dashEffect;
    [SerializeField] private float dashTime = .5f;
    [SerializeField] private float dashDistance = 10f;
    private Vector3 originalScale;

    private Rigidbody2D rb;
    private bool isDashing = false;

    [Header("Loop")]
    private bool jumpLoopEnabled = false;
    private bool dashLoopEnabled = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        inputActions.Player.Jump.performed += ctx =>
        {
            if (IsGrounded())
            {
                Jump();
            }
        };

        inputActions.Player.Dash.performed += ctx =>
        {
            if (!isDashing)
            {
                Dash();
            }
        };
    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }


    private void FixedUpdate()
    {
        if(isDashing)
        {
            return;
        }
        Vector2 moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        Move(moveInput);
    }


    private void Jump()
    {
        if (!jumpLoopEnabled)
        {
            jumpIcon.gameObject.SetActive(true);
            jumpIcon.SetCooldown();
            jumpLoopEnabled = true;
            StartCoroutine(JumpLoop());
        }

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }


    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheckPoint.position, Vector2.down, 0.15f,groundLayer);

        return hit.collider != null;
    }

    private void Dash()
    {
        if(!dashLoopEnabled)
        {
            dashIcon.gameObject.SetActive(true);
            dashIcon.SetCooldown();
            dashLoopEnabled = true;
            StartCoroutine(DashLoop());
        }

        Vector2 dashDirection = new Vector2(transform.localScale.x, 0).normalized;
        isDashing = true;
        dashEffect.SetActive(true);

        StartCoroutine(DashCoroutine(dashDirection));
    }

    private IEnumerator DashCoroutine(Vector2 direction)
    {
        //float gravityScale = rb.gravityScale;
        //rb.gravityScale = 0;
        rb.linearVelocity = direction * dashDistance / dashTime;

        yield return new WaitForSeconds(dashTime);

        //rb.gravityScale = gravityScale;
        rb.linearVelocity = Vector2.zero;
        isDashing = false;
        dashEffect.SetActive(false);
    }

    private void Move(Vector2 moveDir)
    {
        rb.linearVelocity = new Vector2(moveDir.x * moveSpeed, rb.linearVelocity.y);

        if (moveDir.x > 0)
        {
            transform.localScale = originalScale;
        }
        else if (moveDir.x < 0)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, 1);
        }
    }


    #region Loop

    private IEnumerator JumpLoop()
    {
        while (jumpLoopEnabled)
        {
            yield return new WaitForSeconds(jumpIcon.FillTime);
            Jump();
            jumpIcon.gameObject.SetActive(true);
            jumpIcon.SetCooldown();
        }
    }

    private IEnumerator DashLoop()
    {
        while (dashLoopEnabled)
        {
            yield return new WaitForSeconds(dashIcon.FillTime);
            Dash();
            dashIcon.gameObject.SetActive(true);
            dashIcon.SetCooldown();
        }
    }

    #endregion

}
