using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LanguageModule;
using UnityEngine.UI; 

public class Tutoriel1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(PlayerPrefs.GetInt("Tutorial"));
        if (PlayerPrefs.GetInt("Tutorial") == 1 && PlayerPrefs.GetInt("Level") == 0) {
            StartCoroutine(Tuto());
        }
	}

    IEnumerator Tuto() {
        GameObject TutoObject = Instantiate(Resources.Load("TutoText") as GameObject);
        TutoObject.transform.SetParent(GameObject.Find("GameManager").transform, false);
        // GameObject.FindGameObjectWithTag("Player").GetComponent<Personnage>().canMove = false; 
        for (int i = 1; i <= 5; i++) {
            TutoObject.GetComponent<Text>().text = LanguageData.GetString("string" + i, "tutoriel"); 
            yield return new WaitForSeconds(4f);
        }
        TutoObject.GetComponent<Text>().text = LanguageData.GetString("string" + 6, "tutoriel");

        while (Input.GetAxisRaw("Fire1") == 0) {
            yield return new WaitForEndOfFrame();
        }

        for (int i = 7; i < 12; i++) {
            TutoObject.GetComponent<Text>().text = LanguageData.GetString("string" + i, "tutoriel");
            yield return new WaitForSeconds(4f);
        }

        // GameObject.FindGameObjectWithTag("Player").GetComponent<Personnage>().canMove = true;

        TutoObject.GetComponent<Text>().text = LanguageData.GetString("string" + 18, "tutoriel");

        yield return new WaitForSeconds(4f);

        Destroy(TutoObject); 

    }
}
