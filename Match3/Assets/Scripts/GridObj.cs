using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObj : MonoBehaviour {
	SpriteRenderer _renderer;
	Color origColor;
	public enum ColorType {blue, yellow, red}
	public ColorType colortype = ColorType.blue;

	private void Awake ()
	{
		_renderer = GetComponent<SpriteRenderer> ();
		origColor = _renderer.color;
	}
	// Use this for initialization
	void Start () {
		ColorCheck ();
	}

	public void ColorCheck(){
		if (colortype == ColorType.yellow)
			_renderer.color = Color.yellow;
		else if (colortype == ColorType.red)
			_renderer.color = Color.red;
		else
			_renderer.color = origColor;
	}

	public IEnumerator Matched (ColorType col)
	{
		colortype = ColorType.blue;
		for (float i = 0; i <= 1;i+=0.05f)
		{_renderer.color  = Color.Lerp(_renderer.color,Color.white,i) ;
			//Debug.Log ("ad");
			yield return new WaitForSeconds(.02f);
		}
		//yield return new WaitForSeconds (.5f);
		_renderer.color = origColor;
	}
	//public void SetColorType

	// Update is called once per frame
	void Update () {
		
	}
}
