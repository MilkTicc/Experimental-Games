using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
	}

	public void Pongtless(){
		SceneManager.LoadScene ("pong");
	}

	public void Tetrick(){
		SceneManager.LoadScene ("Tetris");
	}

	public void Quit(){
		Application.Quit();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			SceneManager.LoadScene ("MainMenu");
		}
	}
}
