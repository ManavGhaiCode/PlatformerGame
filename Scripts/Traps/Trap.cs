using UnityEngine;

public class Trap : MonoBehaviour {
    protected virtual void OnTriggerEnter2D(Collider2D hitInfo) {
        Player player = hitInfo.GetComponent<Player>();

        if (player != null) {
            Debug.Log("Works");
        }
    }
}
