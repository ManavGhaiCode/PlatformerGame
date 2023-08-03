using UnityEngine;

public class FireTrap : Trap {
    private bool isOn = false;
    private Animator anim;

    public float timeDelay = 3f;

    private void Start() {
        anim = GetComponent<Animator>();
        Invoke("SwitchIsOn", timeDelay);
    }

    private void Update() {
        anim.SetBool("isOn", isOn);
    }
    
    private void SwitchIsOn() {
        isOn = !isOn;
        Invoke("SwitchIsOn", timeDelay);
    }

    protected override void OnTriggerEnter2D(Collider2D hitInfo) {
        if (isOn) {
            base.OnTriggerEnter2D(hitInfo);
        }
    }
}
