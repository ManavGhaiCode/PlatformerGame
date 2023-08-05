using UnityEngine;

public class Enemy : MonoBehaviour {
    public float speed = 5f;

    protected int Health = 10;
    protected Rigidbody2D rb;
    protected GameObject player;

    protected void Start() {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Damage(int damage) {
        Health -= damage;

        if (Health <= 0) {
            Destroy(gameObject);
        }
    }   
}
