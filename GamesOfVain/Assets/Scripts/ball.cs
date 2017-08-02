using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ball : MonoBehaviour {

	public float initSpeed = 5;
	Vector2 Velo;
	Vector2 initPos;
public Text text;
	int score;
	// Use this for initialization
	void Start () {

		float a = Random.Range (.7f, 1f);
		Velo = ( new Vector2( a , Mathf.Sqrt( 1 - a*a) ).normalized * initSpeed);
		Debug.Log(Velo);
		Invoke ("StartBall", .5f);
		score = 0;
		initPos = transform.position;
	}

	void ResetBall(){
		transform.position = initPos;
		Velo = Vector3.zero;
        Invoke ("StartBall", .5f);
	}

	void StartBall(){

		float a = Random.Range (.5f, .8f);
		Velo = (new Vector2 (a, Mathf.Sqrt (1 - a * a)).normalized * initSpeed);
	}


	// Update is called once per frame
	void Update () {
		transform.position += (Vector3)Velo;

		if(Camera.main.WorldToViewportPoint(transform.position).x < 0){
			Debug.Log("asdasdasd");
			score++;
			text.text = score.ToString();
			ResetBall ();
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		Debug.Log(col.gameObject.tag);
		if(col.gameObject.tag == "WallTop"){
			Velo = new Vector3 (Velo.x, -Velo.y,0);
		}
		else if(col.gameObject.tag == "WallBottom"){
			Velo = new Vector3 (Velo.x, -Velo.y,0);
		}
		else if(col.gameObject.tag == "WallMid"){
			Velo = new Vector3 (-Velo.x, Velo.y, 0);
		} else if (col.gameObject.tag == "Player"){
			Velo = new Vector3 (-Velo.x, Velo.y / 1.5f + col.gameObject.GetComponent<playerpaddle> ().Veloy / 2.2f,0);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		
	}
}
