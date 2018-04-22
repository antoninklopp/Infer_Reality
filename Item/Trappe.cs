using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trappe : IRender {

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            GetComponent<Animator>().SetBool("break", true); 
        }
    }

}
