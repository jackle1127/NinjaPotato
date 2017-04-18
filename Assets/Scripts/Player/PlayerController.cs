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

	// Use this for initialization
	void Start () {
        playerMovement = GetComponent<PlayerMovement>();
		spriteGenerator = GameObject.FindGameObjectWithTag ("Sprite Generator").GetComponent<SpriteGenerator> ();
	}
	
	public void TakeDamage(GameObject enemy, float amount, float bounceBackStrength) {
		if (playerMovement.damageState == 0) {
			hp -= amount;
			Vector3 bounceBack = transform.position - enemy.transform.position;
			bounceBack = bounceBack.normalized * bounceBackStrength;
			GetComponent<PlayerMovement> ().TakeDamage (bounceBack);
			spriteGenerator.FlashScreen (damageScreenFlash, -1, dimAlpha);
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
}
