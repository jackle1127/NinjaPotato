using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteGenerator : MonoBehaviour {
	public GameObject[] sparkPrefabs;
	public Camera cam;

	private ScreenFlasher screenFlasher;

	// Use this for initialization
	void Start () {
		screenFlasher = GetComponentInChildren<ScreenFlasher> ();
	}

	public void FlashScreen(Color color, float startingAlpha, float dimAlpha) {
		screenFlasher.Flash (color, startingAlpha, dimAlpha);
	}

	public void Spark(int sparkType, Vector3 position, Quaternion rotation, float size, float dimAlpha) {
		GameObject newSpark = Instantiate (sparkPrefabs [sparkType]);
		newSpark.transform.position = position;
		newSpark.transform.rotation = rotation;
		newSpark.transform.localScale = new Vector3 (size, size, size);
		Spark spark = newSpark.GetComponent<Spark> ();
		spark.alpha = 1;
		spark.dimAlpha = dimAlpha;
	}
}
