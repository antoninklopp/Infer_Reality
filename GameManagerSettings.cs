using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LanguageModule;
using UnityEngine.UI; 

public class GameManagerSettings : MonoBehaviour {

    private GameObject LangueTextChoice;

    private GameObject DifficulteTextChoice;

    private string[] DifficulteString;

    private string[] LanguagesString;

    private int indiceDiff = 1;

    private int indiceLang;

    private bool isLang;

    private bool HorTouched; 

	// Use this for initialization
	void Start () {
        LangueTextChoice = GameObject.Find("LangueTextChoice");
        DifficulteTextChoice = GameObject.Find("DifficulteTextChoice");
        DifficulteString = new string[3] { LanguageData.GetString("string3", "parametres"),
                                            LanguageData.GetString("string5", "parametres"),
                                            LanguageData.GetString("string7", "parametres")};
        LanguagesString = new string[2] {LanguageData.GetString("string10", "parametres"),
                                LanguageData.GetString("string11", "parametres")};
        SelectLang();

        DifficulteTextChoice.GetComponent<Text>().text = DifficulteString[indiceDiff];
        LangueTextChoice.GetComponent<Text>().text = LanguagesString[indiceLang];

    }
	
	// Update is called once per frame
	void Update () {
        GetInput(); 
	}

    public void Retour() {
        SceneManager.LoadScene("Menu");
        PlayerPrefs.SetString("Language", LanguagesString[indiceLang]);
    }

    private void GetInput() {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        
        if (ver > 0) {
            SelectLang();
        } else if (ver < 0) {
            SelectDiff();
        }

        if (isLang) {
            if (hor > 0) {
                if (!HorTouched) {
                    PlusLang();
                    HorTouched = true;
                }
            } else if (hor < 0 ) {
                if (!HorTouched) {
                    MoinsLang();
                    HorTouched = true;
                }
            } else {
                HorTouched = false; 
            }
        } else {
            if (hor > 0) {
                if (!HorTouched) {
                    PlusDiff();
                    HorTouched = true;
                }
            }
            else if (hor < 0){
                if (!HorTouched) {
                    MoinsDiff();
                    HorTouched = true;
                }
            }
            else {
                HorTouched = false;
            }
        }
    }

    public void SelectLang() {
        EnableDisableChoice(LangueTextChoice, true);
        EnableDisableChoice(DifficulteTextChoice, false);
        isLang = true;
        Play(); 
    }

    public void SelectDiff() {
        EnableDisableChoice(LangueTextChoice, false);
        EnableDisableChoice(DifficulteTextChoice, true);
        isLang = false;
        Play();
    }

    public void PlusLang() {
        indiceLang = Mathf.Min(1, indiceLang + 1);
        LangueTextChoice.GetComponent<Text>().text = LanguagesString[indiceLang];
        PlayerPrefs.SetString("Language", LanguagesString[indiceLang]);
        Play(); 
    }

    public void MoinsLang() {
        indiceLang = Mathf.Max(0, indiceLang - 1);
        LangueTextChoice.GetComponent<Text>().text = LanguagesString[indiceLang];
        PlayerPrefs.SetString("Language", LanguagesString[indiceLang]);
        Play(); 
    }

    public void PlusDiff() {
        indiceDiff = Mathf.Min(2, indiceDiff + 1);
        DifficulteTextChoice.GetComponent<Text>().text = DifficulteString[indiceDiff];
        Play(); 
    }

    public void MoinsDiff() {
        indiceDiff = Mathf.Max(0, indiceDiff - 1);
        DifficulteTextChoice.GetComponent<Text>().text = DifficulteString[indiceDiff];
        Play(); 
    }

    private void EnableDisableChoice(GameObject o, bool a_d) {
        o.transform.Find("Left").gameObject.SetActive(a_d);
        o.transform.Find("Right").gameObject.SetActive(a_d);
    }

    private void Play() {
        GameObject.Find("Audio1").GetComponent<AudioSource>().Play(); 
    }
}
