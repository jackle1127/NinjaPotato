using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartTrigger : ItemTrigger {
	public float healAmount;
	protected AudioManager audioManager;

	void Start() {
		audioManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<AudioManager>();
	}

	public override void SignalItemIn(GameObject inObj){
		if(inObj.tag == "Player"){
			audioManager.playCollectHealthItem ();
			inObj.transform.parent.GetComponent<PlayerController> ().Heal (healAmount);
			Destroy (this.gameObject);
		}
	}

	public override void SignalItemOut(GameObject outObj){

	}
}
