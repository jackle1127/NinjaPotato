using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordTrigger : PlayerTrigger {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void signalPlayerIn(GameObject collidingObject) {
        if (collidingObject.tag == "Enemy") {
            playerController.grounded = true;
        }
    }

    public override void signalPlayerOut(GameObject collidingObject) {
        if (collidingObject.tag == "Enemy") {
            playerController.grounded = false;
        }
    }
}
