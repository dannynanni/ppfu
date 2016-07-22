using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerControlPlayer2 : MonoBehaviour {

	public int playerID;

	public float rotationSpeed = 200f;
	public float spinSpeed = 200f;
	//	public Transform spaceShip;
	
	GameObject game;
	bool gameStarted;
	GameObject player2;
	
	private Vector3 inputRotation;
	private Vector3 inputMovement;
	private Vector3 tempVector;
	private Vector3 tempVector2;
	
	public GameObject bullet;
	
	public Transform blaster2;
	
	public AudioSource fire;
	public AudioSource rotate;
	public AudioSource spin;
	public AudioSource death;
	
	float axisInputRotate;
	float axisInputSpin;
	
	
	float leftStickAngle2;
	float rightStickAngle2;
	
	GameLogic gameLogic;

	bool playerIsDead;

	GameObject meshTrail;
	bool disableInput;
	
	// Use this for initialization
	void Start () {
		
		gameStarted = true;
		game = GameObject.Find("GameManager");
		gameLogic = game.GetComponent<GameLogic>();
		gameLogic.StartGame();

		meshTrail = GameObject.Find("Mesh Trail (1)");
		player2 = transform.parent.gameObject;
		
		leftStickAngle2 = 0;
		rightStickAngle2 = 0;

		
		//		ptrScriptVariable = (VariableScript) player.GetComponent( typeof(VariableScript) );
	}
	
	// Update is called once per frame
	void Update () {
		
//		GetJoystickAngles ();
//		
//		if (Input.GetKey (KeyCode.A)) {
//			RotateLeft ();
//			rotate.Play ();
//		}
//		
//		if (Input.GetKey (KeyCode.D)) {
//			RotateRight ();
//			rotate.Play ();
//		}
//		
//		if (Input.GetKey (KeyCode.H)) {
//			SpinLeft ();
//			spin.Play ();
//		}
//		
//		if (Input.GetKey (KeyCode.K)) {
//			SpinRight ();
//			spin.Play ();
//		}
//		
//		//rotate parent based on leftstickangle
//		float newAngle = Mathf.LerpAngle (transform.parent.eulerAngles.z, leftStickAngle2, 0.2f);
//		if (Mathf.Abs (newAngle - leftStickAngle2) < 0.01f) {
//			newAngle = leftStickAngle2;
//		}
//		
//		if (newAngle != transform.parent.eulerAngles.z) {
//			if (!rotate.isPlaying) {
//				rotate.Play ();
//			}
//			transform.parent.eulerAngles = new Vector3 (0, 0, newAngle);
//		} else {
//			if (rotate.isPlaying) {
//				rotate.Stop();
//			}
//		}
		
		//spin based on rightstickangle
//		newAngle = Mathf.LerpAngle (transform.eulerAngles.z, rightStickAngle2, 0.2f);
//		transform.eulerAngles = new Vector3 (0, 0, newAngle);
		
		//Input.GetAxis ("Rotate");
		
		FindPlayerInput ();
		
	}
	
//	void RotateLeft(){
//		//		transform.eulerAngles = Vector3.RotateTowards (transform.eulerAngles, new Vector3 (0f, 0f, 360f), 0f, rotationSpeed * Time.deltaTime);
//		transform.parent.Rotate(new Vector3(0,0,500*Time.deltaTime));
//		leftStickAngle2 = transform.localEulerAngles.z;
//	}
//	
//	void RotateRight(){
//		//		transform.eulerAngles = Vector3.RotateTowards (transform.eulerAngles, new Vector3 (0f, 0f, -0f), 0f, rotationSpeed * Time.deltaTime);
//		transform.parent.Rotate(new Vector3(0,0,-500*Time.deltaTime));
//		leftStickAngle2 = transform.localEulerAngles.z;
//	}
//	
//	void SpinLeft(){
//		transform.eulerAngles += new Vector3 (0f, 0f, spinSpeed * Time.deltaTime);
//		rightStickAngle2 = transform.localEulerAngles.z;
//	}
//	
//	void SpinRight(){
//		transform.eulerAngles -= new Vector3 (0f, 0f, spinSpeed * Time.deltaTime);
//		rightStickAngle2 = transform.localEulerAngles.z;
//	}
	
	public void Reset(){
//		transform.parent.position = new Vector3 (-0f, -0.43f, 0f);
//		transform.localPosition = new Vector3 (0,4.46f,0);
//		transform.localEulerAngles = new Vector3 (0f, 0f, 0f);
//		rightStickAngle2 = 0f;
//		leftStickAngle2 = 0f;
		gameStarted = false;
	}
	
	public void OnTriggerEnter2D (Collider2D col)
	{

		if(col.gameObject.tag == "Enemy")
		{
			GetComponent<ShakeCamera> ().BeginContact (Vector2.zero);
			Destroy(col.gameObject);
			GetComponent<SpriteRenderer>().enabled = false;
			GetComponent<PolygonCollider2D>().enabled = false;
			meshTrail.GetComponent<TrailRenderer>().enabled = false;
			playerIsDead = true;
			disableInput = true;
			Debug.Log("OnCollision");
			death.Play ();
			RespawnDelay();
			ColliderTimer ();
		}

		if(col.gameObject.tag == "Boss")
		{
			GetComponent<ShakeCamera> ().BeginContact (Vector2.zero);
			Destroy(col.gameObject);
			GetComponent<SpriteRenderer>().enabled = false;
			GetComponent<BoxCollider2D>().enabled = false;
			meshTrail.GetComponent<TrailRenderer>().enabled = false;
			playerIsDead = true;
			disableInput = true;
			death.Play ();
			RespawnDelay();
			ColliderTimer ();
			LoadGameOver ();
		}

	}

	void DelayComponents(){
		
		if (playerIsDead) {
			GetComponent<SpriteRenderer> ().enabled = true;
			meshTrail.GetComponent<TrailRenderer>().enabled = true;
			disableInput = false;
			Reset ();
			Debug.Log ("DelayComponents");
		}
		
	}

	void RespawnDelay() {
		Invoke ("DelayComponents", 2.5f);
		Debug.Log ("RespawnDelay");
	}

	void Collider(){
		GetComponent<PolygonCollider2D> ().enabled = true;
		
	}
	
	void ColliderTimer(){
		Invoke ("Collider", 5);
		Debug.Log ("ColliderTimer");
	}
	
	public void FindPlayerInput ()
	{
		if (!disableInput) {
			if (Input.GetButtonDown ("Fire2")) {
				ShootNew ();
				fire.Play ();
			}
		
			if (Input.GetKeyDown (KeyCode.N)) {
				ShootNew ();
				fire.Play ();
			}


//		if (gameLogic.score >= 30f) {
//			if (Input.GetButtonDown ("Fire4")) {
//				ShootNew ();
//				fire.Play ();
//			}
//		}
	}
	}
	void ShootNew ()
	{
		
		GameObject Bullet = (GameObject)
			Instantiate(bullet, blaster2.position,
			            transform.rotation); 
	}
//	void GetJoystickAngles()
//	{
//		
//		float leftX2 = Input.GetAxis ("RotateX2");
//		float leftY2 = Input.GetAxis ("RotateY2");
//		
//		float rightX2 = Input.GetAxis ("SpinX2");
//		float rightY2 = Input.GetAxis ("SpinY2");
//		
//		Vector2 leftStickVector2 = new Vector2 (leftX2, leftY2);
//		Vector2 rightStickVector2 = new Vector2 (rightX2, rightY2);
//		
//		//how far did I move the stick
//		float leftStickDisplacement2 = leftStickVector2.magnitude; //length of the vector
//		float rightStickDisplacement2 = rightStickVector2.magnitude; //length of the vector
//		
//		if (leftStickDisplacement2 > 0.2f) {
//			//update the angle
//			leftStickAngle2 = Mathf.Atan2 (leftY2, leftX2) * Mathf.Rad2Deg + 90f;
//		}
//		
//		if (rightStickDisplacement2 > 0.2f) {
//			rightStickAngle2 = Mathf.Atan2 (rightY2, rightX2) * Mathf.Rad2Deg - 90f;
//		}
//		
//	}

	void GameOverScreen(){
		Application.LoadLevel (2);
	}

	void LoadGameOver(){
		Invoke ("GameOverScreen", 0.5f);
	}
}
