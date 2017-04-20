using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float hp;
	public int maxInvincibilityFrames;
	public Color damageScreenFlash;
	public float dimAlpha;

    private PlayerMovement playerMovement;
	private SpriteGenerator spriteGenerator;
	private GameManager gameManager;
	private AudioManager audioManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
		audioManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<AudioManager>();
        spriteGenerator = GameObject.FindGameObjectWithTag("Sprite Generator").GetComponent<SpriteGenerator>();
        playerMovement = GetComponent<PlayerMovement>();
	}

	void Update() {
		if (playerMovement.damageState == 3 && hp <= 0) {
			this.enabled = false;
			gameManager.Die ();
		}
	}

	public void TakeDamage(GameObject enemy, float amount, float bounceBackStrength) {
		if (playerMovement.damageState == 0) {
			hp -= amount;
			Vector3 bounceBack = transform.position - enemy.transform.position;
			bounceBack = bounceBack.normalized * bounceBackStrength;
			GetComponent<PlayerMovement> ().TakeDamage (bounceBack);
			spriteGenerator.FlashScreen (damageScreenFlash, -1, dimAlpha);
			audioManager.playPlayerHit ();
		}
	}

	public void SetGrounded(bool grounded) {
		playerMovement.grounded = grounded;
	}

	public void SetWallTouching(bool touchingWall) {
		playerMovement.touchingWall = touchingWall;
	}

	public void SwordHit(GameObject hitObject) {
		if (playerMovement.swordActivated && hitObject.tag == "Enemy") {
			Vector2 damageDirection = new Vector2 (Mathf.Cos(playerMovement.currentSwordAngle), 
				Mathf.Sin(playerMovement.currentSwordAngle));
			if (playerMovement.swordDeltaAngle < 0)
				damageDirection = -damageDirection;

			if (playerMovement.facingRight)
				damageDirection.x = -damageDirection.x;

			// Use delta angle as energy
			hitObject.GetComponent<EnemyController> ().DamageEnemy (Mathf.Abs(playerMovement.swordDeltaAngle), damageDirection);
		}
	}

	public void Heal(float amount) {
		hp = Mathf.Clamp (hp + amount, 0, 100);
	}
}
