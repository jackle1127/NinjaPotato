using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour {
	public float hp;
	public Color screenFlashColor;
	public float screenFlashDimAlpha;
	protected SpriteGenerator spriteGenerator;

	public virtual void Start() {
		spriteGenerator = GameObject.FindGameObjectWithTag ("Sprite Generator").GetComponent<SpriteGenerator> ();
	}

	public void Despawn() {
		Destroy (gameObject);
	}

	public abstract void DamageEnemy (float energy, Vector2 direction);
}
