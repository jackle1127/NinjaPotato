using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float maxSpeed;
    public float speedAlpha;
    public float airAcceleration;
    public float jumpPower;
    public float jumpThreshold;
    public float swordAlpha;
    public int maxFrameOnGround;

    public CameraController cameraController;

    //[HideInInspector]
    public bool grounded, touchingWall;
    public Vector2 wallKickRight;
    //[HideInInspector]
    public int damageState;

    const float TAU = Mathf.PI * 2;

    private Rigidbody2D rB;
    private PlayerAnimationController animationController;
    private bool facingRight = true;
    private Vector2 wallKickLeft;
    private float currentSwordAngle;
    private bool swordActivated = false;
    private SwordTrail swordTrail;
    private int frameOnGround;
	// Use this for initialization
	void Start () {
        rB = GetComponent<Rigidbody2D>();
        animationController = GetComponent<PlayerAnimationController>();
        wallKickLeft = wallKickRight;
        wallKickLeft.x = -wallKickLeft.x;
        swordTrail = GetComponentInChildren<SwordTrail>();
	}
	
	// Update is called once per frame
	void Update () {
		if (damageState == 0) {
			float horizontalInput = Input.GetAxisRaw ("Horizontal");
			float verticalInput = Input.GetAxisRaw ("Vertical");
			Vector2 currentVelocity = rB.velocity;
			if (grounded) {
				float targetSpeed = -horizontalInput * maxSpeed;
				currentVelocity.x += (targetSpeed - currentVelocity.x) * speedAlpha;
			} else {
				if (currentVelocity.x * -horizontalInput < 0 || Mathf.Abs (currentVelocity.x) < maxSpeed) {
					currentVelocity.x -= airAcceleration * horizontalInput;
				}
			}
        
			if (grounded) {
				if (horizontalInput != 0) {
					animationController.runTarget = 1;
				} else {
					animationController.runTarget = 0;
				}
			} else {
				animationController.runAlpha = 0;
			}

			if (verticalInput > jumpThreshold && grounded) {
				currentVelocity.y = jumpPower;
				grounded = false;
				animationController.StartJump ();
			}

			if (grounded) {
				animationController.jumpTarget = 0;
			} else {
				animationController.jumpTarget = 1;
			}
			rB.velocity = currentVelocity;

			// Handling sword swinging.
			bool currentSwordActivated = Input.GetAxisRaw ("Sword X") != 0 || Input.GetAxisRaw ("Sword Y") != 0;

			if (currentSwordActivated) {
				float swordX = -Input.GetAxisRaw ("Sword X");
				float swordY = Input.GetAxisRaw ("Sword Y");
				if (!facingRight)
					swordX = -swordX;

				float targetAngle = Mathf.Atan2 (swordX, -swordY); // The angle starts from the bottom.
				targetAngle = (targetAngle + TAU) % TAU; // To wrap around between 0 and TAU.

				if (!swordActivated && currentSwordActivated) { // Sword first activated.
					currentSwordAngle = targetAngle;
				} else if (swordActivated && currentSwordActivated) {
					currentSwordAngle += Mathf.Deg2Rad * Mathf.DeltaAngle (Mathf.Rad2Deg * currentSwordAngle, Mathf.Rad2Deg * targetAngle) * swordAlpha;
				}
				currentSwordAngle = (currentSwordAngle % TAU + TAU) % TAU;
				animationController.airMode = !grounded;
				animationController.swordTime = currentSwordAngle / TAU;
				animationController.swordAlpha = 1;
				swordTrail.trailEnable (true);
			} else {
				animationController.swordAlpha = 0;
				swordTrail.trailEnable (false);
			}

			swordActivated = currentSwordActivated;

			// Wall kicking handling
			if (!grounded && touchingWall) {
				if (verticalInput > jumpThreshold) {
					if (facingRight) {
						if (horizontalInput < 0) {
							rB.velocity = wallKickRight;
							FaceLeft ();
						}
					} else {
						if (horizontalInput > 0) {
							rB.velocity = wallKickLeft;
							FaceRight ();
						}
					}
				}
			}

			// Make player face the direction on the controller
			if (grounded) {
				if (horizontalInput > 0) {
					FaceRight ();
				} else if (horizontalInput < 0) {
					FaceLeft ();
				}
			}
		} else if (damageState == 1) {
			animationController.StartTakingDamage ();
			damageState = 2;
            frameOnGround = maxFrameOnGround;
			animationController.takeDamageAlpha = 1;
			animationController.runAlpha = 0;
			animationController.jumpAlpha = 0;
			animationController.runTarget = 0;
			animationController.jumpTarget = 0;
		} else if (damageState == 2) {
			if (animationController.DamageAnimationComplete ()) {
				if (grounded) {
                    if (frameOnGround > 0) frameOnGround--;
                    else {
                        animationController.StartRecovery();
                        damageState = 3;
                    }
				}
			}
		} else if (damageState == 3) {
			if (animationController.DamageAnimationComplete ()) {
				damageState = 0;
				animationController.takeDamageAlpha = 0;
			}
		}
	}


    private void FaceRight() {
        transform.localScale = new Vector3(1, 1, 1);
        facingRight = true;
        cameraController.FaceRight();
    }

    private void FaceLeft() {
        transform.localScale = new Vector3(-1, 1, 1);
        facingRight = false;
        cameraController.FaceLeft();
    }

	public void TakeDamage(Vector3 bounceBack) {
		rB.velocity = bounceBack;
		damageState = 1;
	}
}
