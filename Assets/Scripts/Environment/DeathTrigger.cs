using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : EnvironmentTrigger {

    public override void SignalEnvironmentIn(GameObject theObject) {
        if (theObject.tag == "Player") {
            environmentController.Die();
        }
    }

    public override void SignalEnvironmentOut(GameObject theObject) {
    }
}
