using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pixel : MonoBehaviour {

    public IEnumerator Flash() {
        float timeTotal = Random.Range(0f, 0.5f);
        yield return new WaitForSeconds(timeTotal); 
        float time = 0;
        bool noir = false; 
        while (timeTotal < 4f) {
            if (timeTotal > 3.8f) {
                time = 5 - timeTotal;
            }  {
                time = Random.Range(0.5f, 1.2f);
            }
            if (noir) {
                GetComponent<SpriteRenderer>().color = Color.black; 
            } else {
                float intensite = Random.Range(0f, 1f);
                GetComponent<SpriteRenderer>().color = new Color(intensite * 1f, intensite * 1f, intensite * 1f); 
            }
            noir = !noir; 
            yield return new WaitForSeconds(time);
            timeTotal += time; 
        }
        Destroy(gameObject); 
    }

    public IEnumerator PixelNoir() {
        Debug.Log("ici"); 
        GetComponent<SpriteRenderer>().color = Color.black; 
        yield return new WaitForSeconds(4f);
        Destroy(gameObject); 
    }

}
