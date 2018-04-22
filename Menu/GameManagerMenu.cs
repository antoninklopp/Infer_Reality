using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class GameManagerMenu : MonoBehaviour {

    /// <summary>
    /// Si true alors le panneau de proposition du tutorial 
    /// de debut est en train d'etre display. 
    /// </summary>
    bool jouerActive = false;

    GameObject JouerButton, Parametres, Credits, QuitterButton, Tutoriel, Oui, Non;
  
    // Use this for initialization
    void Start () {
        JouerButton = GameObject.Find("Jouer");
        Parametres = GameObject.Find("Parametres");
        Credits = GameObject.Find("Credits");
        QuitterButton = GameObject.Find("Quitter");
        Tutoriel = GameObject.Find("Tutoriel");
        Oui = GameObject.Find("Oui");
        Non = GameObject.Find("Non"); 
        EnablePanneauTutoriel(false);

        PlayerPrefs.SetInt("Level", 0);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (EventSystem.current.IsPointerOverGameObject()) {
                return;
            } else {
                if (jouerActive) {
                    jouerActive = false;
                    DactivateAllObject(true);
                    EnablePanneauTutoriel(false); 
                }
            }
        }

        CheckInput(); 
    }

    private void CheckInput() {
        float ax = Input.GetAxisRaw("Horizontal");
        if (ax != 0 && jouerActive) {
            if (ax < 0) {
                Oui.GetComponent<Button>().Select(); 
            } else {
                Non.GetComponent<Button>().Select(); 
            }
        }
    }

    /// <summary>
    /// On propose d'abord au joueur de choisir s'il veut avoir un tutoriel
    /// </summary>
    public void Jouer() {
        jouerActive = true;
        DactivateAllObject(false);

        EnablePanneauTutoriel(true); 
    }

    /// <summary>
    /// Activer ou desactiver les objets de la scene
    /// </summary>
    /// <param name="a_d"></param>
    private void DactivateAllObject(bool a_d) {
        JouerButton.SetActive(a_d);
        Parametres.SetActive(a_d);
        Credits.SetActive(a_d);
        QuitterButton.SetActive(a_d); 
    }

    private void EnablePanneauTutoriel(bool a_d) {
        Tutoriel.SetActive(a_d); 
    }

    public void OuiTutoriel() {
        PlayerPrefs.SetInt("Tutorial", 1);
        SceneManager.LoadScene("level0"); 
    }

    public void NonTutoriel() {
        PlayerPrefs.SetInt("Tutorial", 0);
        SceneManager.LoadScene("level0");
    }

    public void GoToParametres() {
        SceneManager.LoadScene("Settings"); 
    }

    public void GoToCredits() {
        SceneManager.LoadScene("Credits"); 
    }

    public void Quitter() {
        Application.Quit(); 
    }
}
