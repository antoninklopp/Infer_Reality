using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDefaults : MonoBehaviour {

    /// <summary>
    /// Nombre d'images déjà prises
    /// </summary>
    int nombreImages; 

    int nombrePixelsMorts = 0;

    int[,] defaults;

    public GameObject PixelPrefab; 

	// Use this for initialization
	void Start () {
        defaults = new int[10, 18]; 
	}

    /// <summary>
    /// After taking a picture
    /// </summary>
    public void Generate() {
        if (nombreImages < 1) {
            nombreImages++; 
            return; 
        }
        int nombrePixelsClignotants = Mathf.Min(4 * nombreImages, 18*10 - nombrePixelsMorts); 
        // Reperer les pixels clignotants.
        for (int i = 0; i < nombrePixelsClignotants; i++) {
            int x = 0;
            int y = 0; 
            do {
                x = Random.Range(0, 9);
                y = Random.Range(0, 17);
            } while (defaults[x, y] == 1 || defaults[x, y] == -1 || (x < 3 && y < 4));
            defaults[x, y] = 1;
            GenererPixel(x, y); 
        }

        // Reperer les pixels noirs. 
        for (int i = 0; i < 10; i++) {
            for (int j = 0; j < 18; j++) {
                if (defaults[i, j] == -1) {
                    GenererPixelNoir(i, j);
                } else if (defaults[i, j] == 1) {
                    defaults[i, j] = 0; 
                }
            }
        }

        nombreImages++; 
    }

    public void GenerateEmptyBattery() {
        for (int i = 0; i < 2; i++) {
            int x = 0;
            int y = 0;
            do {
                x = Random.Range(0, 9);
                y = Random.Range(0, 17);
            } while (defaults[x, y] == -1 || (x < 3 && y < 4));
            defaults[x, y] = -1;
        }

        Generate(); 
    }

    void GenererPixel(int x, int y) {
        GameObject Pixel = Instantiate(PixelPrefab);
        Camera cam = gameObject.GetComponent<Camera>();
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        Pixel.transform.position = new Vector2(transform.position.x + (y-9) * width / 18, transform.position.y + (x - 5)  * height / 10);
        StartCoroutine(Pixel.GetComponent<Pixel>().Flash()); 
    }

    void GenererPixelNoir(int x, int y) {
        GameObject Pixel = Instantiate(PixelPrefab);
        Camera cam = gameObject.GetComponent<Camera>();
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        Pixel.transform.position = new Vector2(transform.position.x + (y - 9) * width / 18, transform.position.y + (x - 5) * height / 10);
        StartCoroutine(Pixel.GetComponent<Pixel>().PixelNoir());
    }
}
