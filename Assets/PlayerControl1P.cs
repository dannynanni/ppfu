using UnityEngine;
using System.Collections;

public class PlayerControl2 : MonoBehaviour {
	
	public int playerID;
	
	public float rotationSpeed = 200f;
	public float spinSpeed = 200f;
	//	public Transform spaceShip;
	
	GameObject game;
	bool gameStarted;
	GameObject player;
	
	private Vector3 inputRotation;
	private Vector3 inputMovement;
	private Vector3 tempVector;
	private Vector3 tempVector2;
	
	public GameObject bullet;
	
	public Transform blaster;
	
	public AudioSource fire;
	public AudioSource rotate;
	public AudioSource spin;
	public AudioSource death;
	
	float axisInputRotate;
	float axisInputSpin;
	
	
	float leftStickAngle;
	float rightStickAngle;
	
	GameLogic gameLogic;
	
	// Use this for initialization
	void Start () {
		gameStarted = true;
		game = GameObject.Find("GameManager");
		gameLogic = game.GetComponent<GameLogic>();
		gameLogic.StartGame();
		
		player = transform.parent.gameObject;
		
		leftStickAngle = 0;
		rightStickAngle = 0;
		
		//		ptrScriptVariable = (VariableScript) player.GetComponent( typeof(VariableScript) );
	}
	
	// Update is called once per frame
	void Update () {
		
		GetJoystickAngles ();
		
//		if (Input.GetKey (KeyCode.A)) {
//			RotateLeft ();
//			rotate.Play ();
//		}
//		//		} else if (Input.GetKey (KeyCode.D)) {
//		//			RotateRight ();
//		//		}
//		
////		if (Input.GetKey (KeyCode.D)) {
////			RotateRight ();
////			rotate.Play ();
////		}
////		
////		if (Input.GetKey (KeyCode.H)) {
////			SpinLeft ();
////			spin.Play ();
////		}
////		
////		if (Input.GetKey (KeyCode.K)) {
////			SpinRight ();
////			spin.Play ();
////		}
////		
		//rotate parent based on leftstickangle
		float newAngle = Mathf.LerpAngle (transform.parent.eulerAngles.z, leftStickAngle, 0.2f);
		if (Mathf.Abs (newAngle - leftStickAngle) < 0.01f) {
			newAngle = leftStickAngle;
		}
		
		if (newAngle != transform.parent.eulerAngles.z) {
			if (!rotate.isPlaying) {
				rotate.Play ();
			}
			transform.parent.eulerAngles = new Vector3 (0, 0, newAngle);
		} else {
			if (rotate.isPlaying) {
				rotate.Stop();
			}
		}
		
		//spin based on rightstickangle
		newAngle = Mathf.LerpAngle (transform.eulerAngles.z, rightStickAngle, 0.2f);
		transform.eulerAngles = new Vector3 (0, 0, newAngle);
		
		//Input.GetAxis ("Rotate");
		
		FindPlayerInput ();
		
	}
	
	void RotateLeft(){
		//		transform.eulerAngles = Vector3.RotateTowards (transform.eulerAngles, new Vector3 (0f, 0f, 360f), 0f, rotationSpeed * Time.deltaTime);
		transform.parent.Rotate(new Vector3(0,0,500*Time.deltaTime));
		leftStickAngle = transform.localEulerAngles.z;
	}
	
	void RotateRight(){
		//		transform.eulerAngles = Vector3.RotateTowards (transform.eulerAngles, new Vector3 (0f, 0f, -0f), 0f, rotationSpeed * Time.deltaTime);
		transform.parent.Rotate(new Vector3(0,0,-500*Time.deltaTime));
		leftStickAngle = transform.localEulerAngles.z;
	}
	
	void SpinLeft(){
		transform.eulerAngles += new Vector3 (0f, 0f, spinSpeed * Time.deltaTime);
		rightStickAngle = transform.localEulerAngles.z;
	}
	
	void SpinRight(){
		transform.eulerAngles -= new Vector3 (0f, 0f, spinSpeed * Time.deltaTime);
		rightStickAngle = transform.localEulerAngles.z;
	}
	
	public void Reset(){
		transform.parent.position = new Vector3 (-0.16f, -0.43f, 0f);
		transform.localPosition = new Vector3 (0,-3.1f,0);
		transform.localEulerAngles = Vector3.zero;
		rightStickAngle = 0f;
		leftStickAngle = 0f;
		
		gameStarted = false;
	}
	
	public void OnCollisionEnter2D (Collision2D col)
	{
		if(col.gameObject.tag == "Enemy")
		{
			//Destroy(col.gameObject);
			Reset ();
			//gameLogic.Reset ();
			death.Play ();
			//anim.SetTrigger ("explode");
		}
	}
	
	void FindPlayerInput ()
	{
		
		//			tempVector2 = new Vector3(Screen.width * 0.5f,0,Screen.height * 0.5f); 
		//			tempVector = Input.mousePosition; 
		//			tempVector.z = tempVector.y; 
		
		//			tempVector.y = 0;
		//			inputRotation = tempVector - tempVector2; 
		if (Input.GetButtonDown ("Fire1")) {
			Shoot();
			fire.Play ();
		}
		
		if ( Input.GetKeyDown (KeyCode.C))
		{
			Shoot();
			fire.Play();
		}

	}
	void Shoot ()
	{
		
		GameObject Bullet = (GameObject)
			Instantiate(bullet, blaster.position,
			            transform.rotation); 
	}
	void GetJoystickAngles()
	{
		
		float leftX = Input.GetAxis ("RotateX");
		float leftY = Input.GetAxis ("RotateY");
		
		float rightX = Input.GetAxis ("SpinX");
		float rightY = Input.GetAxis ("SpinY");
		
		Vector2 leftStickVector = new Vector2 (leftX, leftY);
		Vector2 rightStickVector = new Vector2 (rightX, rightY);
		
		//how far did I move the stick
		float leftStickDisplacement = leftStickVector.magnitude; //length of the vector
		float rightStickDisplacement = rightStickVector.magnitude; //length of the vector
		
		if (leftStickDisplacement > 0.2f) {
			//update the angle
			leftStickAngle = Mathf.Atan2 (leftY, leftX) * Mathf.Rad2Deg + 90f;
		}
		
		if (rightStickDisplacement > 0.2f) {
			rightStickAngle = Mathf.Atan2 (rightY, rightX) * Mathf.Rad2Deg - 90f;
		}
		
	}
}
