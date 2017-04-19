using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineappleDamageTrigger : EnemyTrigger {
	public override void signalEnemyIn(GameObject collidingObject) {
		if (collidingObject.tag == "Player") {
			((PineappleController)enemyController).DamagePlayer (collidingObject);
		}
	}

    public override void signalEnemyOut(GameObject collidingObject) { }
}
