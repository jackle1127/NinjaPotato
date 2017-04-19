using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSideTrigger : PlayerTrigger {
    private int counter;

	public override void signalPlayerIn(GameObject collidingObject) {
        if (collidingObject.tag == "Terrain") {
            counter++;
			playerController.SetWallTouching(true);
        }
    }

	public override void signalPlayerOut(GameObject collidingObject) {
        if (collidingObject.tag == "Terrain") {
            counter--;
            if (counter <= 0) {
                playerController.SetWallTouching(false);
                counter = 0;
            }
        }
    }
}
