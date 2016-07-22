using UnityEngine;
using System.Collections;

public class newMoveScript : MonoBehaviour {

	public float myStick;
	public float moveRate;
	public float clampSpinRate;
	public float myClamp;
	public float myClampAlt;

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

		myClamp += Time.deltaTime * clampSpinRate;
		if (myClamp >= 360f) {
			myClamp -= 360f;
		}
		Debug.DrawLine (Vector3.zero, new Vector3 (Mathf.Cos(myClamp * Mathf.Deg2Rad), Mathf.Sin(-myClamp * Mathf.Deg2Rad), 0));
	
		StickInput ();

		myStick = (myStick + 360) % 360;
		myClamp = (myClamp + 360) % 360;
		myClampAlt = myClamp + 180;
		myClampAlt = (myClampAlt + 360) % 360;

		transform.parent.rotation = Quaternion.Slerp 
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
					transform.parent.rotation = Quaternion.Euler(new Vector3 (0, 0, myClampAlt));	
				} else {
					transform.parent.rotation = Quaternion.Euler(new Vector3 (0, 0, myClamp));
				}
			} 

//			else if (transform.parent.rotation.eulerAngles.z >= myClampAlt && (transform.parent.rotation.eulerAngles.z >= (myClamp + myClampAlt) / 2f)) 
//			{
//				transform.parent.rotation = Quaternion.Euler(new Vector3 (0, 0, myClampAlt));
//			}

//			if (transform.parent.rotation.eulerAngles.z < 360 && transform.parent.rotation.eulerAngles.z <= myClamp) 
//			{
//				transform.parent.rotation = Quaternion.Euler(new Vector3 (0, 0, myClamp));
//			} 
//			else if (transform.parent.rotation.eulerAngles.z >= 0 && transform.parent.rotation.eulerAngles.z >= myClampAlt) 
//			{
//				transform.parent.rotation = Quaternion.Euler(new Vector3 (0, 0, myClampAlt));
//			}
		}
	}

	void StickInput ()
	{
		float leftX = Input.GetAxis ("RotateX");
		float leftY = Input.GetAxis ("RotateY");
		float rightX = Input.GetAxis ("RotateX2");
		float rightY = Input.GetAxis ("RotateY2");



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


