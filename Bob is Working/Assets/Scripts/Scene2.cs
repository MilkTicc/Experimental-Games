using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene2 : MonoBehaviour {
	public Image currentImage;
	public Sprite anime;
	public Sprite banana;
	public Sprite bananaEp;
	public Sprite coffee;
	public Sprite coffeeEp;
	public Sprite dance1;
	public Sprite dance2;
	public Sprite emma;
	public Sprite phone;
	public Sprite phoneEp;
	public Sprite psvEp;
	public Sprite psv;
	public Sprite thought;
	public Sprite work;
	public Sprite workEP;
	public UI ui;
	public static Scene2 scene2 = null;
	 float energyRate =0.005f;
	 float moodRate = .002f;
	float motivationRate = .006f;
	float energyOffset, moodOffset, motiOffset, moodEPO, motiEOP,energyEOP =0;
	public int hour, minute;
	public Clickables phoneT, phoneH, psvT, psvH, bananaT, coffeeT, bob, EPT;
	public float energyChangeRate, moodChangeRate, motivationChangeRate;
	enum WorkState { Work, Phone, PSV, Dance, Emma, Thought };
	WorkState workState;
	float timeIncreaseInterval = 5;
	float slothInterval = 10;
	float danceInterval = 0.4f;
	bool isWithEarphone;
	int timeSpeed = 1;
	AudioSource source;
	public AudioClip music;

	void Awake(){
		if (scene2 == null)
			scene2 = this;
		else if (scene2 != this)
			Destroy (scene2);

		source = GetComponent<AudioSource> ();
	}
	// Use this for initialization
	void Start ()
	{
		Data.progress = 0;
		Data.mood = 10;
		Data.energy = 35;
		Data.motivation = 5;
		hour = 8;
		minute = 0;
		RandomWorkState ();
		//SwitchWorkState ();
		EnableClickables ();
	}

	void EnableClickables(){
		phoneT.isActive = true;
		psvT.isActive = true;
		bananaT.isActive = true;
		coffeeT.isActive = true;
		bob.isActive = true;
		if(!isWithEarphone)
		EPT.isActive = true;
		
	}

	void DisableClickables(){
		phoneT.isActive = false;
		phoneH.isActive = false;
		psvH.isActive = false;
		psvT.isActive = false;
		bananaT.isActive = false;
		coffeeT.isActive = false;
		bob.isActive = false;
		EPT.isActive = false;
	}

	void SwitchWorkState(){
		switch (workState) {
		case WorkState.Work:
			energyOffset = -.009f;
			moodOffset = -0.003f;
			if (isWithEarphone)
				currentImage.sprite = workEP;
			else
				{currentImage.sprite = work;
			}
			break;
		case WorkState.Phone:
			moodOffset = 0.006f;
			motivationRate = -.003f;
			timeSpeed = 2;
			if (isWithEarphone)
				{currentImage.sprite = phoneEp; 
			}
			else
				{currentImage.sprite = phone; 
			}
			break;
		case WorkState.PSV:
			moodOffset = 0.01f;
			energyOffset = -0.003f;
			motivationRate = -.005f;
			
			timeSpeed = 3;
			if (isWithEarphone)
				{currentImage.sprite = psvEp; 
			}
			else
				{currentImage.sprite = psv; 
			}
			break;
		case WorkState.Dance:
			moodOffset = .01f;
			motiOffset = 0.005f;
			energyRate = -0.003f;
			timeSpeed = 2;
			currentImage.sprite = dance1;
			break;
		case WorkState.Emma:
			moodOffset = 0.012f;
			motiOffset = 0.015f;
			energyOffset = .003f;
			timeSpeed = 5;
			currentImage.sprite = emma;
			break;
		case WorkState.Thought:
			moodOffset = -0.005f;
			motiOffset = 0.01f;
			currentImage.sprite = thought;
			timeSpeed = 1;
			break;
		}
	}

	void RandomWorkState(){
		int workChance = (int)(Data.mood + Data.motivation);
		int phoneChance =(Mathf.Clamp( 120 - (int)(Data.mood + Data.motivation),0,120) + 80)/2;
		int psvChance = (Mathf.Clamp(100 - (int)(Data.mood+ Data.motivation),0,100) + 40) / 2;
		int danceChance = isWithEarphone? Mathf.Clamp((int)(Data.mood + Data.energy / 2), 0 ,40) : 0;
		int emmaChance = isWithEarphone ? 30 : 0;
		int thoughtChance = isWithEarphone ? Mathf.Clamp(( 50 - (int)Data.motivation),0,50) : 0;
		int sum = workChance + psvChance + phoneChance + danceChance + emmaChance + thoughtChance;
		int rand = Random.Range (0, sum);

			//WorkState randomWorkState;
		List<KeyValuePair<WorkState, int>> randomTable = new List<KeyValuePair<WorkState, int>>();
		randomTable.Add (new KeyValuePair<WorkState, int> (WorkState.Work, workChance));
		randomTable.Add (new KeyValuePair<WorkState, int> (WorkState.Phone, phoneChance));
		randomTable.Add (new KeyValuePair<WorkState, int> (WorkState.PSV, psvChance));
		randomTable.Add (new KeyValuePair<WorkState, int> (WorkState.Emma, emmaChance));
		randomTable.Add (new KeyValuePair<WorkState, int> (WorkState.Dance, danceChance));
		randomTable.Add (new KeyValuePair<WorkState, int> (WorkState.Thought, thoughtChance));
		for (int i = 0; i < randomTable.Count; i++){
			if (rand > randomTable [i].Value) {
				rand -= randomTable [i].Value;
			} else
				{workState = randomTable [i].Key;
				//Debug.Log (i);
				break;
			}
			}


		IncreaseHalfTime ();
		EnableClickables ();
		SwitchWorkState ();
	}
	void Dance(){
		if (currentImage.sprite.name == "dance1")
			currentImage.sprite = dance2;
		else
			currentImage.sprite = dance1;
			
	}
	void IncreaseHalfTime ()
	{
		timeIncreaseInterval = 2;
		minute += Random.Range (1,3) * timeSpeed;
		if (minute > 55) {
			minute = 0;
			hour += 1;
		}
		if (hour > 11) {
			hour = 0;
			ui.AMPMChange ();
		}

		ui.TimeChange ();
	}
	void IncreaseTime(){
		timeIncreaseInterval = 4;
		minute += Random.Range(5,8) * timeSpeed;
		if (minute > 59)
		{
			minute = minute - 60;
			hour += 1;
		}
		if (hour > 11)
			{hour = 0;
			UI.instance.AMPMChange ();
		}

		UI.instance.TimeChange ();
	}
	// Update is called once per frame
	void Update () {
		if (isWithEarphone) {
			
		}

		energyChangeRate = energyRate - energyOffset - energyEOP;
		moodChangeRate = moodRate - moodOffset - moodEPO;
		motivationChangeRate =motivationRate + motiOffset + motiEOP;

		Data.energy -= energyChangeRate;
		Data.mood -= moodChangeRate;
		Data.motivation += motivationChangeRate;

		timeIncreaseInterval -= Time.deltaTime;
		if (timeIncreaseInterval < 0)
			IncreaseTime();
		if(workState == WorkState.Work)
		{
			if(Input.anyKeyDown){
				Data.progress += 0.1f * ((Data.energy + Data.mood + Data.motivation) / 90 + .25f);
			}
			slothInterval -= Time.deltaTime;
			if(slothInterval < 0){
				slothInterval = Random.Range(2,6);
				RandomWorkState ();
			}
		}

		if(bananaT.isClicked){
			bananaT.isClicked = false;
			DisableClickables ();
			Data.energy += 5f;

			currentImage.sprite =isWithEarphone? bananaEp: banana;
			Invoke ("RandomWorkState", 2);
		}

		if (coffeeT.isClicked) {
			coffeeT.isClicked = false;
			DisableClickables ();
			Data.energy += 3f;
			Data.mood += 2f;
			Data.motivation += 1f;

			currentImage.sprite = isWithEarphone ? coffeeEp: coffee;
			Invoke ("RandomWorkState", 2);
		}

		if(EPT.isClicked){
			EPT.isClicked = false;
			EPT.isActive = false;
			isWithEarphone = true;
			source.Stop ();
			source.clip = music;
			source.Play ();
			RandomWorkState ();
		}

		if(phoneT.isClicked){
			phoneT.isClicked = false;
			workState = WorkState.Phone;
			IncreaseHalfTime ();
			SwitchWorkState ();
		}
		if(psvT.isClicked){
			psvT.isClicked = false;
			workState = WorkState.PSV;
			IncreaseHalfTime ();
			SwitchWorkState ();
		}

		if(workState==WorkState.Dance){
			danceInterval -= Time.deltaTime;
			if(danceInterval < 0){
				danceInterval = 0.486f;
				Dance ();
			}
		
		}

		if(isWithEarphone){
			energyEOP = .001f;
			moodEPO = .007f;
			motiEOP = .003f;
		}

		if(bob.isClicked){
			bob.isClicked = false;
			RandomWorkState ();
		}

		if(Data.progress >=100){
			SceneManager.LoadScene ("Win");
		}
		else if(hour == 5){
			SceneManager.LoadScene ("Loss");
		}

	}
}
