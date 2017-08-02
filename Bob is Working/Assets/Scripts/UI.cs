using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
	public Image moodBar;
	public Image motivaBar;
	public Image energyBar, progressBar;
	public Text colon;
	public Text hour;
	public Text minute, ampm;
	public static UI instance = null;
	float blickInterval = 1.5f;
	// Use this for initialization
	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		moodBar.transform.localScale = new Vector2 (Data.mood / 100, 1) * 2;
		motivaBar.transform.localScale = new Vector2 (Data.motivation / 100, 1) * 2;
		energyBar.transform.localScale = new Vector2 (Data.energy / 100, 1) * 2;
		progressBar.transform.localScale = new Vector2 (Data.progress / 100, 1) * 2;

		blickInterval -= Time.deltaTime;
		if(blickInterval < 0){
			TextBlick ();
		}

		progressBar.color = new Color ((100 - Data.progress) / 100, 1, (100 - Data.progress) / 100);

		if (Scene2.scene2.moodChangeRate < 0)
			moodBar.color = new Color (1 + Scene2.scene2.moodChangeRate / 0.03f, 1, 1 + Scene2.scene2.moodChangeRate / 0.03f);
		else
			moodBar.color = new Color (1, 1 - Scene2.scene2.moodChangeRate / 0.03f,  1 - Scene2.scene2.moodChangeRate / 0.03f);

		if (Scene2.scene2.energyChangeRate < 0)
			energyBar.color = new Color (1 + Scene2.scene2.energyChangeRate / 0.03f, 1, 1 + Scene2.scene2.energyChangeRate / 0.03f);
		else
			energyBar.color = new Color (1, 1 - Scene2.scene2.energyChangeRate / 0.03f, 1 - Scene2.scene2.energyChangeRate / 0.03f);

		if (Scene2.scene2.motivationChangeRate > 0)
			motivaBar.color = new Color (1 - Scene2.scene2.motivationChangeRate / 0.03f, 1, 1 - Scene2.scene2.motivationChangeRate / 0.03f);
		else
			motivaBar.color = new Color (1, 1 + Scene2.scene2.motivationChangeRate / 0.03f, 1 + Scene2.scene2.motivationChangeRate / 0.03f);
			

		//moodBar.rectTransform.sizeDelta = new Vector2 (Data.mood, moodBar.rectTransform.sizeDelta.y);
	}

	public void AMPMChange(){
		ampm.text = "AM";
	}

	public void TimeChange(){
		hour.text = Scene2.scene2.hour.ToString ();
		minute.text = Scene2.scene2.minute>9? Scene2.scene2.minute.ToString() : "0"+Scene2.scene2.minute.ToString();
	}

	void TextBlick(){
		if (colon.text == ":") {
			blickInterval = .75f;
			colon.text = "";
		}
		else{
			blickInterval = 1.5f;
			colon.text = ":";
		}

	}
}
