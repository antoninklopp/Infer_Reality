using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personnage : IRender {

    public float JumpHeight = 20f;

    public float MoveSpeed = 5f;

    public LayerMask whatIsGround;

    private bool _isGrounded;

    private Transform groundCheck;

    private GameObject Debut;

    private GameObject Fin;

    public bool _isDead;

    public int Life;

    public bool canMove = true;

    private bool immunite = false;

    private bool isWalking; 

	// Use this for initialization
	void Start () {
        groundCheck = transform.Find("GroundCheck");
        Debut = GameObject.Find("Debut");
        Fin = GameObject.Find("Fin");
        StartCoroutine(DebutRoutine()); 
	}

    IEnumerator DebutRoutine() {
        canMove = false; 
        GetComponent<SpriteRenderer>().enabled = false;
        GameObject l = transform.Find("PointLight").gameObject;
        GameObject.Find("PorteDebut").GetComponent<SpriteRenderer>().enabled = false; 
        l.SetActive(false);
        yield return new WaitForSeconds(2f);
        canMove = true; 
        GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("PorteDebut").GetComponent<SpriteRenderer>().enabled = true;
        l.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        if (canMove) {
            Move();
            if (Input.GetButtonDown("Jump")) {
                Jump();
            }
        }
        UpdateState();
        CheckIsDead();
        CheckFin(); 
	}

    private void Move() {
        float x = Input.GetAxis("Horizontal");
        if (x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        } else if (x > 0) {
            transform.localScale = Vector3.one;
        } 

        // Animation
        if (Input.GetButtonDown("Horizontal")) {
            GetComponent<Animator>().SetBool("Run", true);
            GetComponent<SoundPlayer>().PlayFootsteps();
        } 
        if (Input.GetButtonUp("Horizontal")) {
            GetComponent<Animator>().SetBool("Run", false);
            GetComponent<SoundPlayer>().StopFootSteps();
        }
        

        GetComponent<Rigidbody2D>().velocity = new Vector2(MoveSpeed * x, GetComponent<Rigidbody2D>().velocity.y); 
    }

    private void Jump() {
        if (_isGrounded) {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpHeight));
        }
    }

    private void UpdateState() {
        // Check to see if character is grounded by raycasting from the middle of the player
        // down to the groundCheck position and see if collected with gameobjects on the
        // whatIsGround layer
        //if (!_isGrounded) {
            _isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, whatIsGround) && (GetComponent<Rigidbody2D>().velocity.y <= 0);
        //}
    }

    private void CheckIsDead() {
        if (_isDead && !immunite) {
            GameObject Manager = GameObject.Find("GameManager"); 
            Manager.GetComponent<GameManager>().RemoveLife();

            if (Manager.GetComponent<GameManager>().GetNombreCoeurs() == 0) {
                // StartCoroutine(Respawn());
            } else {
                immunite = true; 
                StartCoroutine(Immune()); 
            }
        }
    } 

    private IEnumerator Respawn() {
        transform.position = new Vector2(Debut.transform.position.x, Debut.transform.position.y);
        _isDead = false;
        immunite = true; 
        yield return new WaitForSeconds(2f);
        _isDead = false; 
        immunite = false;
    }

    private IEnumerator Immune() {
        _isDead = false; 
        immunite = true;
        for (int i = 0; i < 10; i++) {
            if (i % 2 == 0) {
                GetComponent<SpriteRenderer>().enabled = false; 
            } else {
                GetComponent<SpriteRenderer>().enabled = true;
            }
            yield return new WaitForSeconds(0.2f); 
        }
        _isDead = false; 
        immunite = false;
    }

    private void CheckFin() {
        if (transform.position.x > Fin.transform.position.x) {
            // On a gagné
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            if (!immunite) {
                _isDead = true;
            }
        }
        if (collision.gameObject.tag == "Fin") {
            StartCoroutine(GameObject.Find("GameManager").GetComponent<GameManager>().FinNiveau());
            collision.gameObject.GetComponent<Animator>().SetBool("Open", true);
            collision.gameObject.GetComponent<AudioSource>().Play();
        }
    }

    public void UpdateLife() {

    }

    public void StopMove() {
        canMove = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<SoundPlayer>().StopFootSteps(); 
    }

}
