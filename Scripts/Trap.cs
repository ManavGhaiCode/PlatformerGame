using UnityEngine;

public class Trap : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D hitInfo) {
        Player player = hitInfo.GetComponent<Player>();

        if (player != null) {
            Debug.Log("Works");
        }
    }
}
