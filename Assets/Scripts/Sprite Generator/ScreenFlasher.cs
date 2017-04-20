using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlasher : MonoBehaviour {
	public float changeAlpha;
    public float target;
    private Image image;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (image.color.a != target) {
			Color color = image.color;
			color.a += (target - color.a) * changeAlpha;
			image.color = color;
		}
	}

	public void Flash(Color color, float startingAlpha, float dimAlpha) {
		// If starting alpha is less than 0, use color's alpha.
		if (startingAlpha >= 0) 
			color.a = startingAlpha;

        target = 0;
		image.color = color;
	}

    public void FadeTo(Color color, float startingAlpha, float targetAlpha, float changeAlpha) {
        // If starting alpha is less than 0, use color's alpha.

        color.a = startingAlpha;
        image.color = color;
        target = targetAlpha;
        this.changeAlpha = changeAlpha;
    }
}
