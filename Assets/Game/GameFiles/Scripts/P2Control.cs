using UnityEngine;
using System.Collections;
using InControl;

public class P2Control : MonoBehaviour {

	public int playerID;

	public float rotationSpeed = 200f;
	public float spinSpeed = 300f;
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

	bool playerIsDead;

	GameObject meshTrail;

	bool disableInput;

	// Use this for initialization
	void Start () {
		gameStarted = true;
		game = GameObject.Find("GameManager");
		gameLogic = game.GetComponent<GameLogic>();
		gameLogic.StartGame();

		meshTrail = GameObject.Find("Mesh Trail");
		player = transform.parent.gameObject;

		leftStickAngle = 0;
		rightStickAngle = 0;


	}

	// Update is called once per frame
	void Update () {


		FindPlayerInput ();

	}



	public void Reset(){
		

		gameStarted = false;
	}

	public void OnTriggerEnter2D (Collider2D col)
	{

		if(col.gameObject.tag == "Enemy")
		{
			Debug.Log ("p2 colliding with enemy");
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
			Debug.Log ("p2 delaying components");

			GetComponent<SpriteRenderer> ().enabled = true;
			meshTrail.GetComponent<TrailRenderer>().enabled = true;
			disableInput = false;
			Reset ();
			Debug.Log ("DelayComponents");
		}

	}

	void RespawnDelay() {
		Invoke ("DelayComponents", 2.5f);
		Debug.Log ("p2 RespawnDelay");
	}

	void Collider(){
		GetComponent<BoxCollider2D> ().enabled = true;

	}

	void ColliderTimer(){
		Invoke ("Collider", 5);
		Debug.Log (" p2 ColliderTimer");
	}

	public void FindPlayerInput ()
	{

		if (!disableInput) {
			if (InputManager.ActiveDevice.RightTrigger.WasPressed) {//Input.GetButtonDown ("Fire2")) {
				Shoot ();
				fire.Play ();
			}

			if (Input.GetKeyDown (KeyCode.C)) {
				Shoot ();
				fire.Play ();
			}
				
		}
	}
	void Shoot ()
	{

		GameObject Bullet = (GameObject)
			Instantiate(bullet, blaster.position,
				transform.rotation); 
	}

	void GameOverScreen(){
		Application.LoadLevel (3);
	}

	void LoadGameOver(){
		Invoke ("GameOverScreen", 0.5f);
	}
}
