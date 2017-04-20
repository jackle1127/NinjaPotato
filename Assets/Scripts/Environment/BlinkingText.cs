using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour {
	public float speed;
	public AnimationCurve curve;

	private Text text;
	private float currentTime;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += speed;
		Color color = text.color;
		color.a = curve.Evaluate (currentTime);
		text.color = color;
	}
}
