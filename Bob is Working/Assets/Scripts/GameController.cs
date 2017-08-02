using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	

	void Start(){
		//Data.mood = 0;
	}

	void DataClamp(){
		Data.mood = Mathf.Clamp (Data.mood, 1, 100);
		Data.motivation = Mathf.Clamp (Data.motivation, 1, 100);
		Data.energy = Mathf.Clamp (Data.energy, 1, 100);
	}

	public void QuitGame(){
		Application.Quit ();
	}

	public void StartGame(){
		SceneManager.LoadScene ("Scene1");
	}
	public void RestartGame ()
	{
		SceneManager.LoadScene ("Menu");
	}
	// Update is called once per frame
	void Update () {

		//if(Input.GetKeyDown(KeyCode.A)){
		//	Data.mood += 10;
		//	Debug.Log (Data.mood);
		//}
		DataClamp ();
	}
}
