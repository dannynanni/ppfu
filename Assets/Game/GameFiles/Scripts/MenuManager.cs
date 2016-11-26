using UnityEngine;
using System.Collections;
using InControl;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if (InputManager.ActiveDevice.Action2.WasPressed) {//Input.GetButtonDown ("Load2P")) {
			StartGame (1);
		}

		if (InputManager.ActiveDevice.Action4.WasPressed) {//Input.GetButtonDown ("ResetGame")) {
			StartGame (0);
		}
	}
	
	public void StartGame (int newLevel){
		Application.LoadLevel (newLevel);

	}
}

