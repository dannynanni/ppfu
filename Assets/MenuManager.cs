using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

//		if (Input.GetButtonDown ("Load1P")) {
//			StartGame(4);
//		}

		if (Input.GetButtonDown ("Load2P")) {
			StartGame (1);
		}

		if (Input.GetButtonDown ("ResetGame")) {
			StartGame (0);
		}
	}
	
	public void StartGame (int newLevel){
		Application.LoadLevel (newLevel);

	}
}

