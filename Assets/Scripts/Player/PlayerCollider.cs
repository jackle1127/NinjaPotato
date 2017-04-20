using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour {
	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Enemy Trigger") {
			collider.gameObject.GetComponent<EnemyTrigger> ().signalEnemyIn (gameObject);
        } else if (collider.gameObject.tag == "Environment Trigger") {
            collider.gameObject.GetComponent<EnvironmentTrigger>().SignalEnvironmentIn(gameObject);
		} else if (collider.gameObject.tag == "Item Trigger") {
			collider.gameObject.GetComponent<ItemTrigger>().SignalItemIn(gameObject);
		}
	}

    public void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.tag == "Enemy Trigger") {
            collider.gameObject.GetComponent<EnemyTrigger>().signalEnemyOut(gameObject);
        } else if (collider.gameObject.tag == "Environment Trigger") {
            collider.gameObject.GetComponent<EnvironmentTrigger>().SignalEnvironmentOut(gameObject);
		} else if (collider.gameObject.tag == "Item Trigger") {
			collider.gameObject.GetComponent<ItemTrigger>().SignalItemOut(gameObject);
		}
    }
}
