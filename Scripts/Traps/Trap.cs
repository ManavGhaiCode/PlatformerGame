using UnityEngine;

public class Trap : MonoBehaviour {
    protected virtual void OnTriggerEnter2D(Collider2D hitInfo) {
        Player player = hitInfo.GetComponent<Player>();

        if (player != null) {
            int KnockbackDir = 0;

            if (player.GetComponent<Transform>().position.x > transform.position.x) {
                KnockbackDir = 1;
            } else if (player.GetComponent<Transform>().position.x < transform.position.x) {
                KnockbackDir = -1;
            }

            player.Knockback(KnockbackDir);
        }
    }
}
