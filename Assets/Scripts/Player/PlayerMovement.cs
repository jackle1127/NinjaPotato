using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float maxSpeed;
    public float speedAlpha;
    public float airAcceleration;
    public float yVelocityAlpha;
    public float jumpPower;
    public float jumpThreshold;
    public float swordAlpha;
    public int maxFrameOnGround;
    public CameraController cameraController;
    public Transform wallKickSparkPosition;
    public float wallKickDimAlpha, wallKickSparkSize;
    public Vector2 wallKickRight;
	public float minSwordWooshDeltaAngle;
	public float maxSwordWooshDeltaAngle;
	public PlayerSwordTrail swordTrail;
    [HideInInspector]
    public bool grounded, touchingWall;
    [HideInInspector]
    public int damageState;
	[HideInInspector]
	public bool facingRight = true;
	[HideInInspector]
	public float swordDeltaAngle, currentSwordAngle;
    [HideInInspector]
    public bool swordActivated;

    const float TAU = Mathf.PI * 2;

    private Rigidbody2D rB;
    private PlayerAnimationController animationController;
    private Vector2 wallKickLeft;
    private bool swordWasActivated = false;
    private int frameOnGround;
    private SpriteGenerator spriteGenerator;
	private AudioManager audioManager;
	public bool swordWooshed;

	// Use this for initialization
	void Start () {
        rB = GetComponent<Rigidbody2D>();
        animationController = GetComponent<PlayerAnimationController>();
        wallKickLeft = wallKickRight;
        wallKickLeft.x = -wallKickLeft.x;
        spriteGenerator = GameObject.FindGameObjectWithTag("Sprite Generator").GetComponent<SpriteGenerator>();
		audioManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<AudioManager>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
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
					animationController.normalizedRunningSpeed = Mathf.Abs (currentVelocity.x) / maxSpeed;
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

            if (!grounded && verticalInput < jumpThreshold && currentVelocity.y > 0) {
                currentVelocity.y *= (1 - yVelocityAlpha);
            }

			if (grounded) {
				animationController.jumpTarget = 0;
			} else {
				animationController.jumpTarget = 1;
			}
			rB.velocity = currentVelocity;

			// Handling sword swinging.
			swordActivated = Input.GetAxisRaw ("Sword X") != 0 || Input.GetAxisRaw ("Sword Y") != 0;

			if (swordActivated) {
				float swordX = -Input.GetAxisRaw ("Sword X");
				float swordY = Input.GetAxisRaw ("Sword Y");
				if (!facingRight)
					swordX = -swordX;

				float targetAngle = Mathf.Atan2 (swordX, -swordY); // The angle starts from the bottom.
				targetAngle = (targetAngle + TAU) % TAU; // To wrap around between 0 and TAU.

				float newSwordAngle = 0;
				if (!swordWasActivated && swordActivated) { // Sword first activated.
					newSwordAngle = targetAngle;
				} else if (swordWasActivated && swordActivated) {
					newSwordAngle = currentSwordAngle + Mathf.Deg2Rad * Mathf.DeltaAngle (Mathf.Rad2Deg * currentSwordAngle, Mathf.Rad2Deg * targetAngle) * swordAlpha;
				}
				newSwordAngle = (newSwordAngle % TAU + TAU) % TAU;

				// Calculate sword energy to use to calculate damage on enemy in the Player Controller.
				swordDeltaAngle = Mathf.Deg2Rad * Mathf.DeltaAngle (Mathf.Rad2Deg * currentSwordAngle, Mathf.Rad2Deg * newSwordAngle);
				bool swordJustSwooshed = Mathf.Abs(swordDeltaAngle) >= minSwordWooshDeltaAngle;
				if (!swordWooshed && swordJustSwooshed) {
					audioManager.playSwordWoosh(Mathf.Lerp(0, 1,
                        Mathf.Clamp01((Mathf.Abs(swordDeltaAngle) - minSwordWooshDeltaAngle) /
							(maxSwordWooshDeltaAngle - minSwordWooshDeltaAngle))));
				}
				swordWooshed = swordJustSwooshed;

				currentSwordAngle = newSwordAngle;
				animationController.airMode = !grounded;
				animationController.swordTime = currentSwordAngle / TAU;
				animationController.swordAlpha = 1;
				swordTrail.trailEnable (true);
			} else {
				animationController.swordAlpha = 0;
				swordTrail.trailEnable (false);
			}

			swordWasActivated = swordActivated;

			// Wall kicking handling
			if (!grounded && touchingWall) {
				if (verticalInput > jumpThreshold) {
					if (facingRight) {
						if (horizontalInput < 0) {
                            spriteGenerator.Spark(1, wallKickSparkPosition.position, new Quaternion(0, 0, 0, 1), wallKickSparkSize, wallKickDimAlpha);
                            rB.velocity = wallKickRight;
							FaceLeft ();
							audioManager.playWallKick ();
						}
					} else {
						if (horizontalInput > 0) {
                            spriteGenerator.Spark(1, wallKickSparkPosition.position, new Quaternion(0, 0, 1, 0), wallKickSparkSize, wallKickDimAlpha);
                            rB.velocity = wallKickLeft;
							FaceRight ();
							audioManager.playWallKick ();
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
			swordActivated = false;
			swordTrail.trailEnable (false);
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

		// Slow down when touching the ground (damageState 2 and 3).
		if (grounded && damageState >= 2) {
			rB.velocity *= (1 - speedAlpha);
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
