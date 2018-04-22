using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {

    public AudioClip footstep;

    public AudioClip Hurt;

    public AudioClip HeartBeat;

    bool heartBeatPlaying = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayFootsteps() {
        if (!heartBeatPlaying) {
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().clip = footstep;
            GetComponent<AudioSource>().Play();
        }
    }

    public void StopFootSteps() {
        GetComponent<AudioSource>().Stop(); 
    }

    public IEnumerator StartHeartBeat() {
        heartBeatPlaying = true;
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().clip = HeartBeat;
        GetComponent<AudioSource>().Play(); 
        yield return new WaitForSeconds(2f);
        GetComponent<AudioSource>().Stop();
        heartBeatPlaying = false; 
    }

    public void HurtPlayer() {
        GetComponent<AudioSource>().clip = Hurt;
        GetComponent<AudioSource>().loop = false; 
        GetComponent<AudioSource>().Play();
    }


}
