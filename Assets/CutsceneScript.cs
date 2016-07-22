﻿using UnityEngine;
using System.Collections;

public class CutsceneScript : MonoBehaviour {

	bool gameStarted;

	// Use this for initialization
	void Start () {
	
		gameStarted = true;


		if (gameStarted) {
			LoadCutscene ();
		}
			
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetButtonDown ("Skip")) {

			Application.LoadLevel (1);
		}
	}

	void Cutscene(){
		Application.LoadLevel (1);
	}
	
	void LoadCutscene(){
		Invoke ("Cutscene", 20.5f);
	}
}