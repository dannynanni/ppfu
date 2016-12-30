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

		//myClamp += Time.deltaTime * clampSpinRate;
		clampCenter += Time.deltaTime * clampSpinRate;
		transform.parent.Rotate(0, 0, Time.deltaTime * clampSpinRate);
		if (myClamp >= 360f) {
			myClamp -= 360f;
		}
		Debug.DrawLine (Vector3.zero, new Vector3 (Mathf.Cos(myClamp * Mathf.Deg2Rad), Mathf.Sin(-myClamp * Mathf.Deg2Rad), 0));
	
		StickInput ();


		myStick = (myStick + 360) % 360;
		myClamp = (myClamp + 360) % 360;
		myClampAlt = myClamp + 180;
		myClampAlt = (myClampAlt + 360) % 360;
		if(clampCenter > 270) {
			myStick += clampCenter - 270;
			if(clampCenter > 450) {
				clampCenter %= 360;
				myStick %= 360;
			}
		}

		if(teleportingAllowed) {
			transform.parent.rotation = Quaternion.Euler(0, 0, myStick);
			}
		else if(Mathf.Abs(transform.parent.rotation.eulerAngles.z - myStick) < 100) {
			transform.parent.rotation = Quaternion.Euler(0, 0, myStick);
		}
		if(transform.parent.rotation.eulerAngles.z > clampCenter + 90) {
			Debug.Log("greater than");
			Debug.Log(transform.parent.rotation.eulerAngles.z);
			transform.parent.rotation = Quaternion.Euler(0, 0, clampCenter + 88);
		}
		else if(transform.parent.rotation.eulerAngles.z < clampCenter - 90) {
			transform.parent.rotation = Quaternion.Euler(0, 0, clampCenter - 88);
		}
/*		transform.parent.rotation = Quaternion.Slerp 
			(transform.parent.rotation, Quaternion.Euler(new Vector3 (0, 0, myStick)), moveRate);

		if (myClampAlt > myClamp) 
		{
			if (transform.parent.rotation.eulerAngles.z <= myClamp) 
			{
				transform.parent.rotation = Quaternion.Euler(new Vector3 (0, 0, myClamp));
			} 
			else if (transform.parent.rotation.eulerAngles.z >= myClampAlt) 
			{
				transform.parent.rotation = Quaternion.Euler(new Vector3 (0, 0, myClampAlt));
			}
		} 
		else
		{
			float middleClamp = (myClamp + myClampAlt) * 0.5f;
			if (transform.parent.rotation.eulerAngles.z < myClamp && transform.parent.rotation.eulerAngles.z > myClampAlt) 
			{
				if (transform.parent.rotation.eulerAngles.z < middleClamp) {
					Debug.Log("myclampAlt");
					transform.parent.rotation = Quaternion.Euler(new Vector3 (0, 0, myClampAlt));	
				} else {
					Debug.Log(myStick);
					transform.parent.rotation = Quaternion.Euler(new Vector3 (0, 0, myClamp));

					Debug.Log(transform.parent.rotation.z);
				}
			} 
				
		}
*/	}

	void StickInput ()
	{
		float leftX = InputManager.ActiveDevice.LeftStick.X;//Input.GetAxis ("RotateX");
		float leftY = InputManager.ActiveDevice.LeftStick.Y;//Input.GetAxis ("RotateY");
		float rightX = InputManager.ActiveDevice.RightStick.X;//Input.GetAxis ("RotateX2");
		float rightY = InputManager.ActiveDevice.RightStick.Y;//Input.GetAxis ("RotateY2");

		Vector2 leftStickVector = new Vector2 (leftX, leftY);
		float leftStickDisplacement = leftStickVector.magnitude; //length of the vector

		Vector2 rightStickVector = new Vector2 (rightX, rightY);
		float rightStickDisplacement = rightStickVector.magnitude; //length of the vector

		if (leftStickDisplacement > 0.3f) {
			 
			if (gameObject.name == "ship") {
//				float leftX2 = Input.GetAxis ("RotateX");
//				float leftY2 = Input.GetAxis ("RotateY");
				myStick = Mathf.Atan2 (leftY, leftX) * Mathf.Rad2Deg;
			}
		}

		if (rightStickDisplacement > 0.3f){

		if (gameObject.name == "ship2") {
//			float rightX2 = Input.GetAxis ("RotateX2");
//			float rightY2 = Input.GetAxis ("RotateY2");
			myStick = Mathf.Atan2 (rightY, rightX) * Mathf.Rad2Deg;
			}
		}


	}
}


