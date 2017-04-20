using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour {
	public float healthChangeAlpha;
	public PlayerController player;
	private Slider slider;
	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();
	}
	
	// Update is called once per frame
	void Update () {
		slider.value += (player.hp - slider.value) * healthChangeAlpha;
	}
}
