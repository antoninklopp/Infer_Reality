using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateforme : IRender {

    GameObject PlayerGround; 

	// Use this for initialization
	void Start () {
        PlayerGround = GameObject.FindGameObjectWithTag("Player").transform.Find("GroundCheck").gameObject; 
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerGround.transform.position.y < transform.position.y) {
            gameObject.layer = 10; 
        } else {
            gameObject.layer = 8; 
        }
	}
}
