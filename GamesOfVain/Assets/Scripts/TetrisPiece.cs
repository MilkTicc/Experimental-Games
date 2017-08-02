using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisPiece : MonoBehaviour {
	SpriteRenderer _renderer;
	Vector3 rotation = Vector3.zero;
	// Use this for initialization
	void Start () {
		_renderer = GetComponent<SpriteRenderer> ();
		InvokeRepeating ("MoveDown", 0,.7f);
	}

	public void SetSprite(Sprite sprite){
		Debug.Log(sprite);
		GetComponent<SpriteRenderer> ().sprite = sprite;
	}

public void MoveDown(){
		transform.position += Vector3.down / 2;
	}

public void TurnTP(){
		Debug.Log("turn");
		
		rotation += new Vector3 (0, 0, 90);
		transform.localRotation= Quaternion.Euler (rotation);
	}

public void MoveLeft ()
	{
		transform.position += Vector3.left / 2;
	}
	public void MoveRight ()
	{
		transform.position += Vector3.right / 2;	}

	// Update is called once per frame
	void Update () {
		
	}
}
