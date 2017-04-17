using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float hp;
	public int maxInvincibilityFrames;
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
			Vector3 bounceBack = transform.position - enemy.transform.position;
			bounceBack = bounceBack.normalized * bounceBackStrength;
			GetComponent<PlayerMovement> ().TakeDamage (bounceBack);
		}
	}
}
