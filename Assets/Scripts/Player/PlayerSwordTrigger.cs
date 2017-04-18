using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordTrigger : PlayerTrigger {
	public override void signalPlayerIn(GameObject collidingObject) {
		playerController.SwordHit (collidingObject);
    }

	public override void signalPlayerOut(GameObject collidingObject) {}
}
