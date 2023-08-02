using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 5f;
    public float JumpForce = 5f;

    private Rigidbody2D rb;

    private float moveInput;
    private bool isGrounded;
    private bool isJumping = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        moveInput = Input.GetAxis("Horizontal");
        isJumping = Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow);

        rb.velocity = new Vector2 (moveInput * speed, rb.velocity.y);

        if (isJumping) {
            rb.velocity = new Vector2 (rb.velocity.x, (Vector2.up * JumpForce).y);
        }
    }
}
