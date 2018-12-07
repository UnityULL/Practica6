using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        GameController.CubeManager += OnCubeThing;
    }

    void OnCubeThing() {
        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        gameObject.GetComponent<Renderer>().material.color = newColor;
    }

}