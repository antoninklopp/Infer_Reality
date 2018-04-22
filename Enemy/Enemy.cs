using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : IRender {

    public GameObject[] Waypoint;

    public bool vertical;
    public bool moving = false;

    private int currentWaypoint = 0;

    public float moveSpeed = 2f; 

	// Use this for initialization
	void Start () {
		if (Waypoint.Length > 0)
        {
            moving = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (moving)
        {
            Move();
        }
    }

    private void Move() {
        if (Waypoint.Length < 2) {
            return; 
        }
        if (vertical) {
            if (Mathf.Abs(Waypoint[currentWaypoint].transform.position.y - transform.position.y) < 0.5f) {
                currentWaypoint = 1 - currentWaypoint;
            }
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, moveSpeed *
                ((transform.position.y < Waypoint[currentWaypoint].transform.position.y) ? 1 : -1));
        } else { 
            if (Mathf.Abs(Waypoint[currentWaypoint].transform.position.x - transform.position.x) < 0.5f) {
                currentWaypoint = 1 - currentWaypoint;
                transform.localScale = new Vector3((Waypoint[currentWaypoint].transform.position.x < transform.position.x) ? -1 : 1, 1, 1); 
            } 
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed *
                ((transform.position.x < Waypoint[currentWaypoint].transform.position.x) ? 1 : -1), 0f);
        }
    }
}
