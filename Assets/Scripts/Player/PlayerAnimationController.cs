using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {
    public float layerWeightAlpha;
	public float maxRunningAnimationSpeed;

	[HideInInspector]
	public float normalizedRunningSpeed;
    [HideInInspector]
    public float runTarget, runAlpha, swordTime, swordAlpha, jumpTarget, jumpAlpha, takeDamageAlpha;
    [HideInInspector]
    public bool airMode;
    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        AnimateLayerWeight(runTarget, ref runAlpha);
        AnimateLayerWeight(jumpTarget, ref jumpAlpha);

        animator.SetLayerWeight(1, runAlpha);
		animator.SetFloat("Running Speed", maxRunningAnimationSpeed * normalizedRunningSpeed);

        animator.SetLayerWeight(2, jumpAlpha);
		animator.SetLayerWeight(3, swordAlpha);
		animator.SetLayerWeight (4, takeDamageAlpha);

        if (airMode) {
            animator.Play("Sword Swinging Air", 3, swordTime);
        } else {
			animator.Play("Sword Swinging", 3, swordTime);
        }
    }

    private void AnimateLayerWeight(float target, ref float alpha) {
        alpha += (target - alpha) * layerWeightAlpha;
    }

    public void StartJump() {
        animator.Play("Jump", 2, 0);
    }

	public void StartTakingDamage() {
		animator.Play("Take Damage", 4, 0);
	}

	public void StartRecovery() {
		animator.Play("Recover", 4, 0);
	}

	public bool DamageAnimationComplete() {
		return animator.GetCurrentAnimatorStateInfo (4).normalizedTime >= 1;
	}

	public void swordHit(GameObject hitObject) {
	}
}
