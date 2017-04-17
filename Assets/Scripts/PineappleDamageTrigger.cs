using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineappleDamageTrigger : EnemyTrigger {
	public override void signalEnemy(GameObject collidingObject) {
		if (collidingObject.tag == "Player") {
			((PineappleController)enemyController).DamagePlayer (collidingObject);
		}
	}
}
