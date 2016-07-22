using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetButtonDown ("Fire1")) {
			StartGame(1);
		}
	}

	public void StartGame (int newLevel){
		Application.LoadLevel (newLevel);
	}
}
