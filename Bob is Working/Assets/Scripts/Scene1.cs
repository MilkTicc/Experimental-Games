using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene1 : MonoBehaviour {
	float time = 0;
	public Image currentImage;
	public Sprite alarmImage;
	public Clickables phone;
	public AudioSource audioSource;
	public AudioClip alarm;
	bool soundPlayed = false;
	// Use this for initialization
	void Start () {
		//phone.enabled = false;
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		if(time > 2f){
			currentImage.sprite = alarmImage;
			phone.isActive = true;

			if (!soundPlayed) {
				soundPlayed = true;
				audioSource.PlayOneShot (alarm);
			}
		}

		if(phone.isClicked){
			phone.isClicked = false;
			SceneManager.LoadScene ("game");
		}
	}
}
