﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBottomTrigger : PlayerTrigger {
	public override void signalPlayerIn(GameObject collidingObject) {
		if (collidingObject.tag == "Terrain" || collidingObject.tag == "Enemy") { // You can also step on enemies.
			playerController.SetGrounded(true);
        }
    }

	public override void signalPlayerOut(GameObject collidingObject) {
		if (collidingObject.tag == "Terrain" || collidingObject.tag == "Enemy") {
			playerController.SetGrounded(false);
        }
    }
}
