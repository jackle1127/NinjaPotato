using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyTrigger : MonoBehaviour {
	public EnemyController enemyController;
	public abstract void signalEnemy(GameObject theObject);
}
