using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 5f;
    public float JumpForce = 5f;

    public int _ExtraJumps = 1;

    private Rigidbody2D rb;
    private Animator anim;

    private float moveInput;
    private bool isGrounded;
    private int FacingDir = 1;
    private bool canMove = true;
    private bool isWallDetected;
    private bool isJumping = false;
    private bool isFacingRight = true;
    private bool wasWallSliding = false;

    private bool canWallSlide = false;
    private bool isWallSliding = false;

    [SerializeField] private Transform GroundCheck;
    [SerializeField] private Transform WallCheck;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private float CheckRadious = .3f;

    [SerializeField] private float coyoteTime = .2f;

    private int ExtraJumps = 1;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        ExtraJumps = _ExtraJumps;
    }

    private void Update() {
        moveInput = Input.GetAxis("Horizontal");
        isJumping = Input.GetKeyDown("z");

        bool _isGrounded = Physics2D.OverlapCircle(GroundCheck.position, CheckRadious, Ground);
        bool isWallDetected = Physics2D.OverlapCircle(WallCheck.position, CheckRadious, Ground);

        if (isWallDetected && rb.velocity.y < 0) {
            canWallSlide = true;
        } else {
            canWallSlide = false;
        }

        isWallSliding = canWallSlide && Input.GetKey("x");

        if (isWallSliding) {
            rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y * .1f);
            wasWallSliding = true;
        } else {
            Invoke("UnWallSliding", .2f);
        }

        anim.SetBool("isWallSliding", isWallSliding);

        Debug.Log(isWallDetected);

        if (_isGrounded) {
            isGrounded = true;
            ExtraJumps = _ExtraJumps;

            anim.SetBool("isGrounded", _isGrounded);
        } else {
            Invoke("UnGround", coyoteTime);

            anim.SetBool("isGrounded", _isGrounded);
        }

        if (isJumping) {
            if (wasWallSliding) {
                WallJump();
            } else {
                if (isGrounded) {
                    rb.velocity = new Vector2 (rb.velocity.x, (Vector2.up * JumpForce).y);
                } else if (ExtraJumps > 0) {
                    rb.velocity = new Vector2 (rb.velocity.x, (Vector2.up * JumpForce).y);
                    ExtraJumps -= 1;
                }
            }

        }

        if (isFacingRight && moveInput < 0) {
            Flip();
        } else if (!isFacingRight && moveInput > 0) {
            Flip();
        }

        if (moveInput != 0) {
            anim.SetBool("isRunning", true);
        } else {
            anim.SetBool("isRunning", false);
        }

        anim.SetFloat("Y_velocity", Mathf.Clamp(rb.velocity.y, -1, 1));
    }

    private void FixedUpdate() {
        if (!isWallSliding && canMove) {
            rb.velocity = new Vector2 (moveInput * speed, rb.velocity.y);
        }
    }

    private void UnGround() {
        isGrounded = false;
    }

    private void WallJump() {
        canMove = false;
        rb.velocity = new Vector2 (3 * -FacingDir, JumpForce);

        Invoke("ResetCanMove", .2f);
    }

    private void UnWallSliding() {
        wasWallSliding = false;
    }

    private void ResetCanMove() {
        canMove = true;
    }

    private void Flip() {
        if (!isWallSliding) {
            FacingDir *= -1;

            transform.Rotate(0, 180, 0);
            isFacingRight = !isFacingRight;
        }
    }
}
