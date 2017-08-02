using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof(Collider2D))]
public class Clickables : MonoBehaviour {
	public Texture2D cursor;
	public bool isActive = false;
	public bool isClicked = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseOver(){
		if(isActive)
		Cursor.SetCursor (cursor, Vector2.zero, CursorMode.Auto);

		if (Input.GetMouseButtonDown (0) & isActive)
			isClicked = true;
	}

	void OnMouseExit(){
		Cursor.SetCursor (null, Vector2.zero, CursorMode.Auto);
	}

	void OnDrawGizmos(){
		Gizmos.color = new Color (1f, 1f, 0f, .8f);
		Vector2 size = new Vector2 (GetComponent<BoxCollider2D> ().size.x * transform.localScale.x, GetComponent<BoxCollider2D> ().size.y * transform.localScale.y);
		Gizmos.DrawCube (transform.position, size);
	}
}
