using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coeur : IRender {

    GameObject child; 

	// Use this for initialization
	void Start () {
        child = transform.Find("coeur").gameObject; 
        StartCoroutine(TourneRoutine()); 
	}

    private IEnumerator TourneRoutine() {
        while (true) {
            yield return new WaitForSeconds(0.1f);
            child.transform.Rotate(new Vector3(0, 0, 6)); 
        }
    }

    public override void SetRenderIR(float f) {
        DefaultMaterial = child.GetComponent<MeshRenderer>().material;
        child.GetComponent<MeshRenderer>().material = Instantiate(Resources.Load("CoeurIR") as Material); 
    }

    public override IEnumerator ResetRender() {
        yield return new WaitForSeconds(2f);
        child.GetComponent<MeshRenderer>().material = DefaultMaterial;
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        GameObject Manager = GameObject.Find("GameManager"); 
        if (collision.gameObject.tag == "Player") {
            if (Manager.GetComponent<GameManager>().GetNombreCoeurs() < 3) {
                Manager.GetComponent<GameManager>().AddCoeur();
                GameObject Particles = Instantiate(Resources.Load("Particle/CoeurParticle") as GameObject);
                Particles.transform.SetParent(transform);
                Particles.transform.localPosition = Vector2.zero;
                StartCoroutine(DestroyParticles(Particles));
                Destroy(child.GetComponent<MeshRenderer>());
                Destroy(GetComponent<BoxCollider2D>());
            } else {
                Destroy(gameObject); 
            }
        }
    }

    private IEnumerator DestroyParticles(GameObject Part) {
        yield return new WaitForSeconds(1f);
        Destroy(Part);
        Destroy(gameObject);
    }
}
