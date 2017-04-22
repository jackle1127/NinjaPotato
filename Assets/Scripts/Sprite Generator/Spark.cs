using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour {
	public float despawnAlpha, alpha, dimAlpha;

	private Material material;

	void Start() {
		material = GetComponent<Renderer>().material;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (alpha > despawnAlpha) {
			alpha = (1 - dimAlpha) * alpha;
			material.color = new Color (1, 1, 1, alpha);
		} else {
			Destroy (gameObject);
		}
	}
}
