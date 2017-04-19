using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBottomTrigger : PlayerTrigger {
    private int counter;

	public override void signalPlayerIn(GameObject collidingObject) {
		if (collidingObject.tag == "Terrain" || collidingObject.tag == "Enemy") { // You can also step on enemies.
            counter++;
			playerController.SetGrounded(true);
        }
    }

	public override void signalPlayerOut(GameObject collidingObject) {
		if (collidingObject.tag == "Terrain" || collidingObject.tag == "Enemy") {
            counter--;
            if (counter <= 0) {
                playerController.SetGrounded(false);
                counter = 0;
            }
        }
    }
}
