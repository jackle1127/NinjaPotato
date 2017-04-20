using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    public float alpha;
    public Transform cameraLeftBound, cameraRightBound;
    private GameObject playerTarget;

	// Use this for initialization
	void Start () {
        playerTarget = transform.GetChild(0).GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 cameraPosition = transform.position;
        cameraPosition += alpha * (player.transform.position - playerTarget.transform.position);
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, cameraRightBound.position.x, cameraLeftBound.position.x);
        transform.position = cameraPosition;
	}

    public void FaceRight() {
        transform.GetChild(0).gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    public void FaceLeft() {
        transform.GetChild(0).gameObject.transform.localScale = new Vector3(-1, 1, 1);
    }
}
