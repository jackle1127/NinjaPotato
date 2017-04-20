using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapToPlayerTrigger : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player Trigger") {
            collider.gameObject.GetComponent<PlayerTrigger>().signalPlayerIn(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player Trigger") {
            collider.gameObject.GetComponent<PlayerTrigger>().signalPlayerOut(gameObject);
        }
    }

}
