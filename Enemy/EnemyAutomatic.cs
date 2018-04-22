using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAutomatic : MonoBehaviour {

    private GameObject CheckPlatform;

    public int moveSpeed = 2; 

    /// <summary>
    /// 1 pour aller vers la droite, -1 pour aller vers la gauche
    /// </summary>
    private int side;

	// Use this for initialization
	void Start () {
        CheckPlatform = transform.Find("CheckPlatform").gameObject; 
	}
	
	// Update is called once per frame
	void Update () {
        Move(); 
	}

    private void Move() {
        Collider2D[] overlaping = Physics2D.OverlapCircleAll(CheckPlatform.transform.position, 0.01f);

        if (overlaping.Length == 0) {
            // Dans le cas où il n'y a plus de plateforme en dessous de l'enemi
            // on le fait se retourner
            side *= (-1);
            // On change aussi le sprite. 
            transform.localScale = new Vector3(side, 1, 1); 
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed * side, GetComponent<Rigidbody2D>().velocity.y); 
    }
}
