using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    GameObject Player;

    public GameObject WayPoint1;

    public GameObject WayPoint2; 

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player"); 		
	}
	
	// Update is called once per frame
	void Update () {
        if ((Player.transform.position.x < WayPoint2.transform.position.x) && (Player.transform.position.x > WayPoint1.transform.position.x)) {
            transform.position = new Vector3(Player.transform.position.x + 2, Player.transform.position.y + 1, transform.position.z);
        } else {
            transform.position = new Vector3(transform.position.x, Player.transform.position.y + 1, transform.position.z);
        }
	}
}
