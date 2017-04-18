using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlasher : MonoBehaviour {
	public float dimAlpha;
	private Image image;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (image.color.a > 0) {
			Color color = image.color;
			color.a = (1 - dimAlpha) * color.a;
			image.color = color;
		}
	}

	public void Flash(Color color, float startingAlpha, float dimAlpha) {
		// If starting alpha is less than 0, use color's alpha.
		if (startingAlpha >= 0) 
			color.a = startingAlpha;
		
		image.color = color;
		this.dimAlpha = dimAlpha;
	}
}
