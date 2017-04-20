using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	public AudioSource theme;
	public AudioSource swordHit;
	public AudioSource swordWoosh;
	public AudioSource collectHealthItem;
	public AudioSource playerHit;
	public AudioSource wallKick;

	public void playTheme() {
		theme.Play ();
	}

	public void playSwordHit() {
		swordHit.Play ();
	}

	public void playSwordWoosh(float volume) {
		swordWoosh.volume = volume;
		swordWoosh.Play ();
	}

	public void playCollectHealthItem() {
		collectHealthItem.Play ();
	}

	public void playPlayerHit() {
		playerHit.Play ();
	}

	public void playWallKick() {
		wallKick.Play ();
	}
}
