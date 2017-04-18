using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerTrigger : MonoBehaviour {
	public PlayerController playerController;
	public abstract void signalPlayerIn(GameObject collidingObject);
	public abstract void signalPlayerOut(GameObject collidingObject);
}
