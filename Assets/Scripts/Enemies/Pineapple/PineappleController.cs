using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineappleController : EnemyController {
	public float damage;
	public float playerBounceBackStrength;
	public float energyThreshold;
	public float bounceBackMultiplier;
	public int framesAfterDeath;
	public Transform playerTransform;
	public float sparkDimAlpha;
	public float sparkSize;
	public Transform sparkPosition;
	private bool alive = true;
	private Animator animator;
	private float currentScale;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		animator = GetComponentInChildren<Animator> ();
		currentScale = Mathf.Abs(transform.localScale.x);
	}
	
	void FixedUpdate () {
		if (alive) {
			if (playerTransform.position.x > transform.position.x) {
				transform.localScale = new Vector3 (-currentScale, currentScale, currentScale);
			} else {
				transform.localScale = new Vector3 (currentScale, currentScale, currentScale);
			}
		} else {
			if (framesAfterDeath > 0) {
				framesAfterDeath--;
			} else {
				Despawn ();
			}
		}
	}

	public void DamagePlayer(GameObject playerCollider) {
		if (alive) {
			// Player Collider is child of the player object where the Player Controller is.
			playerCollider.transform.parent.GetComponent<PlayerController> ()
				.TakeDamage (gameObject, damage, playerBounceBackStrength);
		}
	}

	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Player Trigger") {
			collider.gameObject.GetComponent<PlayerTrigger> ().signalPlayerIn (gameObject);
		}
	}

	public override void DamageEnemy (float damageEnergy, Vector2 direction) {
		if (alive && damageEnergy > energyThreshold) {
			alive = false;
			spriteGenerator.FlashScreen (screenFlashColor, -1, screenFlashDimAlpha);
			Quaternion sparkRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Rad2Deg * (Mathf.Atan2(direction.y, direction.x) - 90)));
            spriteGenerator.Spark(0, sparkPosition.position, sparkRotation, sparkSize, sparkDimAlpha);
			GetComponentInChildren<CapsuleCollider2D> ().enabled = false; // Disable collider.
			Vector2 bounceBack = bounceBackMultiplier * damageEnergy * direction;
			bounceBack.y = Mathf.Max (bounceBack.y, 0);
			GetComponent<Rigidbody2D> ().velocity = bounceBack;
			animator.SetLayerWeight (1, 1);
			animator.Play ("Dying", 1, 0);
		}
	}
}
