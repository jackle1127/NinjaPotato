using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineappleNotificationTrigger : EnemyTrigger {
    public override void signalEnemyIn(GameObject collidingObject) {
        if (collidingObject.tag == "Player") {
            ((PineappleController)enemyController).playerTransform = collidingObject.transform;
        }
    }

    public override void signalEnemyOut(GameObject collidingObject) {
        if (collidingObject.tag == "Player") {
            ((PineappleController)enemyController).playerTransform = null;
        }
    }
}
