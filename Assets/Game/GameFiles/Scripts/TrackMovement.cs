using UnityEngine;
using System.Collections;

public class TrackMovement : MonoBehaviour {

	public float clampSpinRate;
	public float myClamp;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
		myClamp += Time.deltaTime * clampSpinRate;
		if (myClamp >= 360) {
			myClamp = 0;
		}

		transform.rotation = Quaternion.Euler(new Vector3 (0, 0, myClamp));
	}
}
