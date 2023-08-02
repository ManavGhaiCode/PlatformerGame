using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 5f;
    public float JumpForce = 5f;

    public int _ExtraJumps = 1;

    private Rigidbody2D rb;
    private Animator anim;

    private float moveInput;
    private bool isGrounded;
    private bool isJumping = false;
    private bool isFacingRight = true;

    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private float GroundRadious = .3f;

    [SerializeField] private float coyoteTime = .2f;

    private int ExtraJumps = 1;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        ExtraJumps = _ExtraJumps;
    }

    private void Update() {
        moveInput = Input.GetAxis("Horizontal");
        isJumping = Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow);

        bool _isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundRadious, Ground);

        if (_isGrounded) {
            isGrounded = true;
            ExtraJumps = _ExtraJumps;

            anim.SetBool("isGrounded", _isGrounded);
        } else {
            Invoke("UnGround", coyoteTime);

            anim.SetBool("isGrounded", _isGrounded);
        }

        if (isJumping) {
            if (isGrounded) {
                rb.velocity = new Vector2 (rb.velocity.x, (Vector2.up * JumpForce).y);
            } else if (ExtraJumps > 0) {
                rb.velocity = new Vector2 (rb.velocity.x, (Vector2.up * JumpForce).y);
                ExtraJumps -= 1;
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
        rb.velocity = new Vector2 (moveInput * speed, rb.velocity.y);
    }

    private void UnGround() {
        isGrounded = false;
    }

    private void Flip() {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }
}
