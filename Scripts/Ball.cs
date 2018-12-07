using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameController.BallManager += OnBallThing;
	}

	void OnBallThing () {
        Vector3 scale = transform.localScale;
        scale.x += 0.1f;
        scale.y += 0.1f;
        scale.z += 0.1f;

        transform.localScale = scale;
    }
}
