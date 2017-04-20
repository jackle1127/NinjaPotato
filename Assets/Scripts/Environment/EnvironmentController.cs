using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour {
    GameManager gameManager;

    void Start() {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
    }

    public void WinLevel() {
        gameManager.Win();
    }

	public void Die() {
		gameManager.Die();
	}
}
