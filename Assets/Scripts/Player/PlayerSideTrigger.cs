using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSideTrigger : PlayerTrigger {
	public override void signalPlayerIn(GameObject collidingObject) {
        if (collidingObject.tag == "Terrain") {
			playerController.SetWallTouching(true);
        }
    }

	public override void signalPlayerOut(GameObject collidingObject) {
        if (collidingObject.tag == "Terrain") {
			playerController.SetWallTouching(false);
        }
    }
}
