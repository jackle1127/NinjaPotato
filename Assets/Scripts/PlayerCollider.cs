﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour {
	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Enemy Trigger") {
			collider.gameObject.GetComponent<EnemyTrigger> ().signalEnemy (gameObject);
		}
	}
}