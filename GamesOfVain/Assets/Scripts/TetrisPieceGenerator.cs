using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetrisPieceGenerator : MonoBehaviour
{

	public TetrisPiece piecePrefab;
	public Sprite [] sprites;

 Sprite nextSprite;
	public SpriteRenderer nextSpriterdr;
	TetrisPiece currentTP;

	void CreateRandomPiece ()
	{
		TetrisPiece piece = Instantiate<TetrisPiece> (piecePrefab);
		piece.transform.position = transform.position;
		piece.SetSprite (nextSprite);
		nextSprite = sprites [Random.Range (0, sprites.Length - 1)];
		nextSpriterdr.sprite = nextSprite;
		//image.sprite = nextSprite;
		piece.gameObject.SetActive (true);
		currentTP = piece;
	}

	// Use this for initialization
	void Start ()
	{
		nextSprite = sprites [Random.Range (0, sprites.Length - 1)];
		nextSpriterdr.sprite = nextSprite;
		
		//image.sprite = nextSprite;
        Invoke ("CreateRandomPiece", 1f);
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space))
			currentTP.TurnTP ();
		if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A) )
			currentTP.MoveLeft ();
		if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D) )
			currentTP.MoveRight ();
		if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S) )
			currentTP.MoveDown ();

		if(currentTP!=null){
			if (Camera.main.WorldToViewportPoint (currentTP.transform.position).y < 0) {
				Destroy (currentTP.gameObject);
				CreateRandomPiece ();
			}
		}
	}


}
