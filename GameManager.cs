using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI; 

public class GameManager : MonoBehaviour {

    private GameObject ParentBatterie;

    private GameObject ParentCoeur; 

    public GameObject BatteriePrefab;

    private List<GameObject> AllBatteries = new List<GameObject>();

    private List<GameObject> AllCoeurs = new List<GameObject>();

    public GameObject CoeurPrefab;

    private GameObject GlobalLight;

    private int BatteriesPleines;

    private GameObject Chrono;

    private bool takingPicture;

    private bool _isBatteryRegen;

    public int ThisLevel;

    private bool isInPause;

    public GameObject UIPause; 

    private bool fin;

	// Use this for initialization
	void Start () {
        ParentBatterie = GameObject.Find("ParentBatterie");
        ParentCoeur = GameObject.Find("ParentCoeur");
        GlobalLight = GameObject.Find("GlobalLight");
        Chrono = GameObject.Find("Chrono");
        UIPause = GameObject.Find("UIPause");
        UIPause.SetActive(false); 
        GlobalLight.SetActive(false);
        AfficherNiveau(); 
        GenerateBatteries(5);
        GenerateCoeurs(3);
        // StartCoroutine(GenerateEnergy());
        StartCoroutine(BeginChrono()); 
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
            TakePicture(); 
        }
        if (Input.GetButtonDown("Cancel")) {
            Pause(); 
        }
	}

    /// <summary>
    /// Creation des batteries
    /// </summary>
    private void GenerateBatteries(int n) {
        // Offset 1 : 10 -10
        // Offset 2 : 25 -25
        for (int i = 0; i < n; i++) {
            AddBatterie(); 
        }
    }

    public void AddBatterie() {
        GameObject NewBatterie = Instantiate(BatteriePrefab);
        NewBatterie.transform.SetParent(ParentBatterie.transform, false);
        NewBatterie.GetComponent<RectTransform>().localPosition = new Vector2(10 + AllBatteries.Count * 19.4f, 0);
        AllBatteries.Add(NewBatterie);
        if (AllBatteries.Count > 1) {
            NewBatterie.GetComponent<Animator>().SetBool("Second", true); 
        } 
        AllBatteries[BatteriesPleines].GetComponent<Animator>().SetBool("Reload", true);
        BatteriesPleines++; 
    }

    private void RemoveBattery() {
        BatteriesPleines--; 
        AllBatteries[BatteriesPleines].GetComponent<Animator>().SetBool("Reload", false);
        StartCoroutine(RegenBattery()); 
    }

    void GenerateCoeurs(int n) {
        for (int i = 0; i < n; i++) {
            AddCoeur();
        }
    }

    public void AddCoeur() {
        if (AllCoeurs.Count < 3) {
            GameObject NewCoeur = Instantiate(CoeurPrefab);
            NewCoeur.transform.SetParent(ParentCoeur.transform, false);
            NewCoeur.GetComponent<RectTransform>().localPosition = new Vector2(10 + AllCoeurs.Count * 25, 0);
            AllCoeurs.Add(NewCoeur);
        }
    }

    private IEnumerator RegenBattery() {
        if (!_isBatteryRegen) {
            _isBatteryRegen = true; 
            int i = 0;
            while (i < 10) {
                if (!takingPicture && !isInPause) {
                    i++;
                }
                yield return new WaitForSeconds(1f);
            }
        } else {
            yield break; 
        }

        _isBatteryRegen = false; 

        AllBatteries[BatteriesPleines].GetComponent<Animator>().SetBool("Reload", true);
        BatteriesPleines++;

        // On regen encore une batterie si il en reste à regenerer. 
        if (BatteriesPleines < AllBatteries.Count) {
            StartCoroutine(RegenBattery()); 
        }
    }


    public void TakePicture() {

        // On ne peut pas prendre deux images d'un coup. 
        if (takingPicture || isInPause || BatteriesPleines == 0) {
            return;
        }

        GameObject.Find("Photo").GetComponent<AudioSource>().Play(); 

        // Le joueur doit s'arreter
        GameObject.FindGameObjectWithTag("Player").GetComponent<Personnage>().StopMove(); 

        GlobalLight.SetActive(true);
        GlobalLight.GetComponent<Light>().intensity = 1; 
        IRender[] allRenderedObjects = FindObjectsOfType<IRender>(); 
        foreach(IRender i in allRenderedObjects) {
            i.SetRenderIR(); 
        }

        RemoveBattery(); 

        if (BatteriesPleines == 0) {
            // Sur la derniere batterie
            GameObject.Find("DefaultCamera").GetComponent<GenerateDefaults>().GenerateEmptyBattery();
        } else {
            GameObject.Find("DefaultCamera").GetComponent<GenerateDefaults>().Generate();
        }

        // Dire au personnage de s'arreter

        StartCoroutine(StopIR()); 
    }

    private IEnumerator StopIR() {
        takingPicture = true; 
        yield return new WaitForSeconds(4f);

        IRender[] allRenderedObjects = FindObjectsOfType<IRender>();
        foreach (IRender i in allRenderedObjects) {
            StartCoroutine(i.ResetRender());
        }

        for (int i = 0; i < 10; i++) {
            if (1 - 2 * i / 10f <= 0) {
                GlobalLight.SetActive(false);
            }
            GlobalLight.GetComponent<Light>().intensity = 1 - 2 * i / 10f;
            yield return new WaitForSeconds(0.1f); 
        }

        GlobalLight.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Personnage>().canMove = true;
        takingPicture = false; 
    }

    /// <summary>
    /// Ajoute une barre de batterie toutes les dix secondes
    /// </summary>
    /// <returns></returns>
    private IEnumerator GenerateEnergy() { 
        while(true){
            yield return new WaitForSeconds(10f); 

            if (BatteriesPleines < AllBatteries.Count) {
                // Recherger
                AllBatteries[BatteriesPleines].GetComponent<Animator>().SetBool("Reload", true);
                BatteriesPleines++; 
            }
        }
    }

    public void RemoveLife() {
        if (AllCoeurs.Count == 1) {
            // Fin, mort
            Death(); 
        } else {
            GameObject RemoveCoeur = AllCoeurs[AllCoeurs.Count - 1];
            AllCoeurs.RemoveAt(AllCoeurs.Count - 1);
            if (AllCoeurs.Count == 2) {
                GameObject.FindGameObjectWithTag("Player").GetComponent<SoundPlayer>().StartHeartBeat();
            } else {
                GameObject.FindGameObjectWithTag("Player").GetComponent<SoundPlayer>().HurtPlayer();
            }
            Destroy(RemoveCoeur); 
        }
    }

    private void Death() {
        SceneManager.LoadScene("Mort");
    }

    public int GetNombreCoeurs() {
        return AllCoeurs.Count; 
    }

    private IEnumerator BeginChrono() {
        int i = 20 * (PlayerPrefs.GetInt("Level") + 1);
        if (PlayerPrefs.GetInt("Level") == 0) {
            i = 360; 
        }
        while (i > 0) { 
            yield return new WaitForSeconds(1f);
            if (!takingPicture && !isInPause) {
                Chrono.GetComponent<Text>().text = ((int)(i / 60)).ToString() + ":" + (i % 60).ToString("00");
                i--;
            }
        }

        // Fin du temps. 
        Death(); 
    }

    public IEnumerator FinNiveau() {
        if (!fin) {
            fin = true; 
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            if (PlayerPrefs.GetInt("Level") == 12) {
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene("Win");
            }
            else {
                AsyncOperation op = SceneManager.LoadSceneAsync("level" + PlayerPrefs.GetInt("Level").ToString());
                op.allowSceneActivation = false;
                yield return new WaitForSeconds(1f);
                op.allowSceneActivation = true;
            }
        }
    }

    public void Pause() {
        if (takingPicture) {
            return; 
        }

        if (isInPause) {
            // On enleve de pause
            isInPause = false;
            UIPause.SetActive(false); 
        } else {
            isInPause = true;
            UIPause.SetActive(true);
        }
    }

    public void Reprendre() {
        Pause(); 
    }

    public void Recommencer() {
        SceneManager.LoadScene("level" + PlayerPrefs.GetInt("Level").ToString());
    }

    public void Quitter() {
        SceneManager.LoadScene("Menu");
    }

    private void AfficherNiveau() {
        // Numerotage niveau commence à partir de 0
        Debug.Log(PlayerPrefs.GetInt("Level"));
        GameObject.Find("AffichageNiveau").GetComponent<Text>().text = SceneManager.GetActiveScene().name;//"Level " + (PlayerPrefs.GetInt("Level") + 1).ToString();
    }
}
