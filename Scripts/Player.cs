using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 5f;
    public float JumpForce = 5f;

    private Rigidbody2D rb;

    private float moveInput;
    private bool isGrounded;
    private bool isJumping = false;

    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private float GroundRadious = .3f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        moveInput = Input.GetAxis("Horizontal");
        isJumping = Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow);

        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundRadious, Ground);
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2 (moveInput * speed, rb.velocity.y);

        if (isJumping) {
            rb.velocity = new Vector2 (rb.velocity.x, (Vector2.up * JumpForce).y);
        }
    }
}
