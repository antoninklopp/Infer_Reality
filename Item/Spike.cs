using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : Item {

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            GetComponent<Animator>().SetBool("GoUp", true);
            collision.gameObject.GetComponent<Personnage>()._isDead = true;
            StartCoroutine(StopAnimation());
            Debug.Log("collision");
            // GetComponent<Animator>().SetBool("GoUp", false);
        }    
    }

    private IEnumerator StopAnimation() {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Animator>().SetBool("GoUp", false);
    }

}
