using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemTrigger : MonoBehaviour {

	public abstract void SignalItemIn (GameObject inObj);
	public abstract void SignalItemOut (GameObject outObj);

}
