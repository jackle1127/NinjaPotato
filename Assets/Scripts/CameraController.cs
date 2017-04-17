using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    public float alpha;

    private GameObject playerTarget;

	// Use this for initialization
	void Start () {
        playerTarget = transform.GetChild(0).GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
       	transform.position += alpha * (player.transform.position - playerTarget.transform.position);
	}

    public void FaceRight() {
        transform.GetChild(0).gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    public void FaceLeft() {
        transform.GetChild(0).gameObject.transform.localScale = new Vector3(-1, 1, 1);
    }
}
