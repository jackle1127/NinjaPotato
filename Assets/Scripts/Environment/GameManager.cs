using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	const int MODE_PLAYING = 0;
	const int MODE_DEAD = 1;
	const int MODE_WIN = 2;
	public float fadeAlpha;
	public GameObject player;

    private SpriteGenerator spriteGenerator;
    public int currentWaitFrames;
    private int mode = 0;
	// Use this for initialization
	void Start () {
        spriteGenerator = GameObject.FindGameObjectWithTag("Sprite Generator").GetComponent<SpriteGenerator>();
	}

	void FixedUpdate() {
		if (mode != 0) {
			// Disable movement.
			player.GetComponent<PlayerMovement> ().enabled = false;
			if (currentWaitFrames > 0) currentWaitFrames--;
			else {
				player.GetComponent<PlayerMovement> ().enabled = true;
				if (mode == MODE_DEAD) {
					SceneManager.LoadScene ("Level 1");
				} else if (mode == MODE_WIN) {
					SceneManager.LoadScene ("Ending");
				}
			}
		}
	}

	private int setWaitFrames(float alpha) {
		return (int) Mathf.Log (.01f, 1 - alpha);
	}

    public void Die() {
		spriteGenerator.FadeTo(new Color(0, 0, 0), 0, 1, fadeAlpha);
		currentWaitFrames = setWaitFrames (fadeAlpha);
        mode = MODE_DEAD;
    }

    public void Win() {
		spriteGenerator.FadeTo(new Color(1, 1, 1), 0, 1, fadeAlpha);
		currentWaitFrames = setWaitFrames (fadeAlpha);
		mode = MODE_WIN;
    }
}
