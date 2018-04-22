using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Item {

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            GameObject.Find("GameManager").GetComponent<GameManager>().AddBatterie();
            Destroy(gameObject); 
        }
    }

}
