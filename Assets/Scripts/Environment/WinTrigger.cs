using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : EnvironmentTrigger {

    public override void SignalEnvironmentIn(GameObject theObject) {
        if (theObject.tag == "Player") {
            environmentController.WinLevel();
        }
    }

    public override void SignalEnvironmentOut(GameObject theObject) {
    }
}
