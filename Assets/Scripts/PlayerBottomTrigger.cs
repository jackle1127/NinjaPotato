using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBottomTrigger : PlayerTrigger {
    public override void signalPlayerIn(GameObject collidingObject) {
        if (collidingObject.tag == "Terrain") {
            playerController.grounded = true;
        }
    }

    public override void signalPlayerOut(GameObject collidingObject) {
        if (collidingObject.tag == "Terrain") {
            playerController.grounded = false;
        }
    }
}
