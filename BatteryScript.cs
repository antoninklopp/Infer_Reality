using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : IRender {

    GameObject child;

    // Use this for initialization
    void Start() {
        child = transform.Find("batterie").gameObject;
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
        child.GetComponent<MeshRenderer>().material = Instantiate(Resources.Load("BatterieIR") as Material);
    }

    public override IEnumerator ResetRender() {
        yield return new WaitForSeconds(2f);
        child.GetComponent<MeshRenderer>().material = DefaultMaterial;
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            GameObject.Find("GameManager").GetComponent<GameManager>().AddBatterie();
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        GameObject Manager = GameObject.Find("GameManager");
        if (collision.gameObject.tag == "Player") {
            Manager.GetComponent<GameManager>().AddBatterie();
            GameObject Particles = Instantiate(Resources.Load("Particle/EclairParticle") as GameObject);
            Particles.transform.SetParent(transform);
            Particles.transform.localPosition = Vector2.zero;
            StartCoroutine(DestroyParticles(Particles));
            foreach (Transform t in child.transform) {
                Destroy(t.gameObject);
            }
            Destroy(GetComponent<BoxCollider2D>());
        }
    }

    private IEnumerator DestroyParticles(GameObject Part) {
        yield return new WaitForSeconds(1f);
        Destroy(Part);
        Destroy(gameObject);
    }

}
