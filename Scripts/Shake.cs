using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour {

    private Vector3 originPosition;
    private Quaternion originRotation;
    public float shake_decay = 0.002f;
    public float shakeIntensity = .3f;

    private float shaking = 0;

    // Use this for initialization
    void Start() {
        GameController.CapsuleManager += ShakeIt;
    }

    // Update is called once per frame
    void Update() {
        if (shaking > 0) {
            transform.position = originPosition + Random.insideUnitSphere * shaking;
            transform.rotation = new Quaternion(
                originRotation.x + Random.Range(-shaking, shaking) * .2f,
                originRotation.y + Random.Range(-shaking, shaking) * .2f,
                originRotation.z + Random.Range(-shaking, shaking) * .2f,
                originRotation.w + Random.Range(-shaking, shaking) * .2f);
            shaking -= shake_decay;
        }
    }

    public void ShakeIt() {
        originPosition = transform.position;
        originRotation = transform.rotation;
        shaking = shakeIntensity;
    }
}
