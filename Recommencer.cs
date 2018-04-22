using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Recommencer : MonoBehaviour {

    public void Restart() {
        SceneManager.LoadScene("level" + PlayerPrefs.GetInt("Level")); 
    }

}
