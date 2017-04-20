using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnvironmentTrigger : MonoBehaviour {
	public EnvironmentController environmentController;
	public abstract void SignalEnvironmentIn(GameObject theObject);
    public abstract void SignalEnvironmentOut(GameObject theObject);
}
