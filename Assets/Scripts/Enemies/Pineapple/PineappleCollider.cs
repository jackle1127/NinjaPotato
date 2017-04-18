using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineappleCollider : MonoBehaviour {
	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Player Trigger") {
			collider.gameObject.GetComponent<PlayerTrigger> ().signalPlayerIn (gameObject);
		}
	}
}
