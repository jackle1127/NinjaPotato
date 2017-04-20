using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour {
	public float hp;
	public Color screenFlashColor;
	public float screenFlashDimAlpha;
	protected SpriteGenerator spriteGenerator;
	protected AudioManager audioManager;

	public virtual void Start() {
		spriteGenerator = GameObject.FindGameObjectWithTag ("Sprite Generator").GetComponent<SpriteGenerator> ();
		audioManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<AudioManager>();
	}

	public void Despawn() {
		Destroy (gameObject);
	}

	public abstract void DamageEnemy (float energy, Vector2 direction);
}
