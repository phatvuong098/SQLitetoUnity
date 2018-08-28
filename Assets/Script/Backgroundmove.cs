using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgroundmove : MonoBehaviour {
    Vector2 starPos;
    public float speed=5f;
	// Use this for initialization
	void Start () {
        starPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        float newPos = Mathf.Repeat(Time.time*speed, 12);
        transform.position = starPos + Vector2.down * newPos;
	}
}
