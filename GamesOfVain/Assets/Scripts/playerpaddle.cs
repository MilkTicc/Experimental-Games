using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerpaddle : MonoBehaviour {

	Vector3 velo;
	public float speed;
	public float Veloy{ get { return velo.y;}}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		velo = Vector2.zero;

		velo = new Vector3 (0, speed * Input.GetAxis ("Vertical"),0);
		//if(transform.position.y < 6 )
		transform.position += velo;
	}
}
