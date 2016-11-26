using UnityEngine;
using System.Collections;
using InControl;

public class Cutscene2Script : MonoBehaviour {

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

		if (InputManager.ActiveDevice.AnyButton) {// Input.GetButtonDown ("Skip")) {

			Application.LoadLevel (2);
		}
		
	}
	
	void Cutscene(){
		Application.LoadLevel (2);
	}
	
	void LoadCutscene(){
		Invoke ("Cutscene", 20.5f);
	}
}