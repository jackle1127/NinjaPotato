using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public float hp;
	public int maxInvincibilityFrames;
	public Slider healthBar;
    private PlayerMovement playerMovement;

	// Use this for initialization
	void Start () {
        playerMovement = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void TakeDamage(GameObject enemy, float amount, float bounceBackStrength) {
		if (playerMovement.damageState == 0) {
			hp -= amount;
			healthBar.value = hp;
			Vector3 bounceBack = transform.position - enemy.transform.position;
			bounceBack = bounceBack.normalized * bounceBackStrength;
			GetComponent<PlayerMovement> ().TakeDamage (bounceBack);
		}
	}
}
