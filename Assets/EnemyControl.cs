using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {
	

	Animator anim;

	GameObject mothership;
	GameObject game;
	GameObject player;
	PlayerControl2 playerNew;

	//float moveForce= 0.4f;
	float moveForce = 0.09999999f;
		//Random.Range (0.2f,0.1f);
	
	float timeSinceSpawn;
	float moveDelay = 0.01f; 
	bool hasPlayedSound;
	bool collisionTest;
	bool collisionTest2;
	private float timeSpentAlive;
	public GameObject particle;	


	void Start () {
	
		mothership = GameObject.Find("mothership");
		game = GameObject.Find("GameManager");
		anim = GetComponent<Animator> ();
		timeSinceSpawn = 0;
		hasPlayedSound = false;

		player = GameObject.Find("ship");
		playerNew = player.GetComponent<PlayerControl2>();
	} 

	void Update () {

		Vector2 mothershipPosition = mothership.transform.position; 
		Vector2 thisPosition = transform.position;

		Vector2 vectorToMothership = mothershipPosition - thisPosition;

		Vector2 directionToMothership = vectorToMothership.normalized;

		Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

		float angleToMothership = Mathf.Atan2(directionToMothership.y, directionToMothership.x) * Mathf.Rad2Deg - 90;
				
		rigidBody.MoveRotation(angleToMothership);


		timeSinceSpawn += Time.deltaTime; 


		if (timeSinceSpawn > moveDelay) {

			rigidBody.AddForce (directionToMothership * moveForce);
			if (hasPlayedSound == false) { 
				hasPlayedSound = true;
			}
		}

			else if (transform.position.x >= -10 && transform.position.x <=10){
				Debug.Log ("xRange");
				rigidBody.AddForce (directionToMothership * 0.0001f);
			if (hasPlayedSound == false) { 
				hasPlayedSound = true;
			}
			}
				


		timeSpentAlive += Time.deltaTime;
		if (timeSpentAlive > 11f) 
		{
			Destroy(gameObject);
		}


	}


	void OnTriggerEnter2D (Collider2D thisCollision){

		if (thisCollision.gameObject.tag == "Bullet") {
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
		Invoke ("GameOver", 0.3f);
	}
}


