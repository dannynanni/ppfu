using UnityEngine;
using System.Collections;
using InControl;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (InputManager.ActiveDevice.RightTrigger.WasPressed) {// Input.GetButtonDown ("Fire1")) {
			StartGame(1);
		}
	}

	public void StartGame (int newLevel){
		Application.LoadLevel (newLevel);
	}
}
