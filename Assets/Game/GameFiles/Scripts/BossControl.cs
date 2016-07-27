using UnityEngine;
using System.Collections;

public class BossControl : MonoBehaviour {

	public GameObject boss;
	public int bossHealth;
	GameObject mgr;
	GameLogic gameLogic;
	Animator anim;
	GameObject mothership;
	GameObject game;
	GameObject player;
	//PlayerControl2 playerNew;
	float moveForce = 8f;
	float timeSinceSpawn;
	float moveDelay = 0.2f; 
	bool hasPlayedSound;
	bool collisionTest = false;
	bool collisionTest2;
	private float timeSpentAlive;
	public GameObject particle;	
	public AudioSource cometSound;

	// Use this for initialization
	void Start () {

		mgr = GameObject.Find ("GameManager");
		gameLogic = mgr.GetComponent<GameLogic>();
		mothership = GameObject.Find("mothership");
		anim = GetComponent<Animator> ();
		timeSinceSpawn = 0;
		hasPlayedSound = false;
		player = GameObject.Find("ship");
		//playerNew = player.GetComponent<PlayerControl2>();

	}

	// Update is called once per frame
	void Update () {

		Vector2 mothershipPosition = mothership.transform.position; 
		Vector2 thisPosition = transform.position;
		Vector2 vectorToMothership = mothershipPosition - thisPosition;
		Vector2 directionToMothership = vectorToMothership.normalized;
		Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
		float angleToMothership = Mathf.Atan2(directionToMothership.y, directionToMothership.x) * Mathf.Rad2Deg - 90;
		rigidBody.MoveRotation(angleToMothership);
		timeSinceSpawn += Time.deltaTime; 

		if (timeSinceSpawn > moveDelay){

			rigidBody.AddForce(directionToMothership*moveForce);
			if (hasPlayedSound == false){ 
				//				GetComponent<AudioSource>().Play();
				hasPlayedSound = true;
			}
		}

		timeSpentAlive += Time.deltaTime;

		if (timeSpentAlive > 19f) 
		{
			//Destroy(gameObject);
			DeathAnimationTimer();
		}

	}

	void OnTriggerEnter2D (Collider2D thisCollision){
		Debug.Log ("outside If" + bossHealth);
		if (thisCollision.gameObject.tag == "Bullet") {
			Debug.Log ("inside If" + bossHealth);
			cometSound.Play ();
			Instantiate (particle, this.transform.position, this.transform.rotation);
			bossHealth -= 1;
			Debug.Log ("new boss health " + bossHealth);

		}

		if (bossHealth <= 0) {
			Debug.Log ("inside If 2" + bossHealth);
			collisionTest = true;
		}

		if (collisionTest) {
			anim.SetTrigger ("explode");
			Instantiate (particle, this.transform.position, this.transform.rotation);
			DeathAnimationTimer ();
		}

	}

	void OnTriggerEnter2D2 (Collider2D thisCollision){

		if (thisCollision.gameObject.tag == "player") {
			collisionTest2 = true;
			Debug.Log ("Enemy Player Collision");

		}

		if (collisionTest2) {
			anim.SetTrigger ("explode");
			DeathPlayerTimer();
		}

	}

	void DestroyObject(){
		gameLogic.bossAlive = false;
		gameLogic.StartZoomIn ();
		Debug.Log ("bossALiveFALSE");
		Instantiate (particle, this.transform.position, this.transform.rotation);
		Destroy (gameObject);
	}

	void DeathAnimationTimer(){
		GetComponent<BoxCollider2D> ().enabled = false;
		Invoke ("DestroyObject", 0.5f);
	}

	void DeathPlayerTimer(){

		Invoke ("DestroyObject", 1);
	}

	void GameOverScreen(int newLevel){
		Application.LoadLevel (3);
	}

	void LoadGameOver(){
		Invoke ("GameOverScreen", 0.3f);
	}
}

