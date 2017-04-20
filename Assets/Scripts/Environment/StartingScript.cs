using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingScript : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw("Start") > 0) {
			SceneManager.LoadScene ("Level 1");
		}
	}
}
