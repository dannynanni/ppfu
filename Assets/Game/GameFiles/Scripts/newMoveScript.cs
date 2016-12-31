using UnityEngine;
using System.Collections;
using InControl;

public class newMoveScript : MonoBehaviour {

	public float myStick;
	public float moveRate;
	public float clampSpinRate;
	public float myClamp;
	public float myClampAlt;
	public float clampCenter;
	public float rotationPosition;
	public bool teleportingAllowed;

	// Use this for initialization
	void Start () {
		myStick = 0;
		if (gameObject.name == "ship2") {
			myClamp = 180;
			myStick = 180;
		}

	}
	
	// Update is called once per frame
	void Update () {

		clampCenter += Time.deltaTime * clampSpinRate;
		rotationPosition += Time.deltaTime * clampSpinRate;
		if (myClamp >= 360f) {
			myClamp -= 360f;
		}
		Debug.DrawLine (Vector3.zero, new Vector3 (Mathf.Cos(myClamp * Mathf.Deg2Rad), Mathf.Sin(-myClamp * Mathf.Deg2Rad), 0));
	
		StickInput ();


		myStick = (myStick + 360) % 360;

		if(clampCenter > 270) {
			if(myStick < 90) {
				myStick += 360;
			}
			if(clampCenter > 450) {
				clampCenter %= 360;
				myStick %= 360;
			}
		}

		if(teleportingAllowed) {
			rotationPosition = myStick;
			}
		else if(Mathf.Abs(rotationPosition - myStick) < 100) {
			rotationPosition = myStick;
		}
		if(rotationPosition > clampCenter + 90) {
			rotationPosition = clampCenter + 88;
			transform.parent.rotation = Quaternion.Euler(0, 0, clampCenter + 88);
		}
		else if(rotationPosition < clampCenter - 90) {
			rotationPosition = clampCenter - 88;
		}
		rotationPosition += Time.deltaTime * clampSpinRate;
		transform.parent.rotation = Quaternion.Euler(0, 0, rotationPosition % 360);
	}

	void StickInput ()
	{
		float leftX = InputManager.ActiveDevice.LeftStick.X;
		float leftY = InputManager.ActiveDevice.LeftStick.Y;
		float rightX = InputManager.ActiveDevice.RightStick.X;
		float rightY = InputManager.ActiveDevice.RightStick.Y;

		Vector2 leftStickVector = new Vector2 (leftX, leftY);
		float leftStickDisplacement = leftStickVector.magnitude; //length of the vector

		Vector2 rightStickVector = new Vector2 (rightX, rightY);
		float rightStickDisplacement = rightStickVector.magnitude; //length of the vector

		if (leftStickDisplacement > 0.3f) {
			 
			if (gameObject.name == "ship") {
				myStick = Mathf.Atan2 (leftY, leftX) * Mathf.Rad2Deg;
			}
		}

		if (rightStickDisplacement > 0.3f){

		if (gameObject.name == "ship2") {
			myStick = Mathf.Atan2 (rightY, rightX) * Mathf.Rad2Deg;
			}
		}


	}
}


