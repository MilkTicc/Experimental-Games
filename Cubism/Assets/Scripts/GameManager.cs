using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public PlayerController player;
	public Transform goal;
	List<string> sceneNames;
	// Use this for initialization
	void Start () {
		sceneNames = new List<string> () { "Title", "Cubism 1", "Cubism 2", "Cubism 3" ,"Cubism 4","Cubism 5"};
	}


	// Update is called once per frame
	void Update () {
		if(player.transform.position.y < -5 )
		{
			SceneManager.LoadScene (sceneNames [sceneNames.IndexOf (SceneManager.GetActiveScene ().name)]);
			
		}

		float _disToGoal = (player.transform.position - goal.position).magnitude;
		if(_disToGoal<1.2f){
			if(sceneNames.IndexOf (SceneManager.GetActiveScene ().name) < sceneNames.Count -1)
				SceneManager.LoadScene (sceneNames [sceneNames.IndexOf (SceneManager.GetActiveScene ().name) + 1]);
			//SceneManager.LoadScene (sceneNames [sceneNames.IndexOf (SceneManager.GetActiveScene ().name) ]);
			else{
				SceneManager.LoadScene (sceneNames [0]);
				
			}
		}
		
	}
}
