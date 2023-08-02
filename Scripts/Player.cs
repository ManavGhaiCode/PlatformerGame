using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 5f;

    private float moveInput;

    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        moveInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate() {
        rb.velocity = new Vector2 (moveInput * speed, rb.velocity.y);
    }
}
