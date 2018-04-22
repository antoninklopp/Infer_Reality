using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class RetourMenu : MonoBehaviour {

    public void Retour() {
        SceneManager.LoadScene("Menu"); 
    }

}
