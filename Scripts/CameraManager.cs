using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour {
    public string playerTag;

    [SerializeField] private GameObject Cam;
    [SerializeField] private PolygonCollider2D cd;
    [SerializeField] private Color GizmosColor;

    private void Start() {
        Cam.GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindGameObjectWithTag(playerTag).transform;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo) {
        Player player = hitInfo.GetComponent<Player>();

        if (player != null) {
            Cam.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D hitInfo) {
        Player player = hitInfo.GetComponent<Player>();

        if (player != null) {
            Cam.SetActive(false);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = GizmosColor;
        Gizmos.DrawWireCube(cd.bounds.center, cd.bounds.size);
    }
}
