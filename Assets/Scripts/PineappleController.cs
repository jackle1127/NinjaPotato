using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineappleController : Enemy {
	public float damage;
	public float playerBounceBackStrength;

	private bool alive = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DamagePlayer(GameObject player) {
		if (alive) {
			player.transform.parent.GetComponent<PlayerController> ().TakeDamage (gameObject, damage, playerBounceBackStrength);
		}
	}
}
