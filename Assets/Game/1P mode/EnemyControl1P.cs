using UnityEngine;
using System.Collections;

public class EnemyControl1P : MonoBehaviour {
	

	Animator anim;

	GameObject mothership;
	GameObject game;
	GameObject player;
	PlayerControl2 playerNew;

	float moveForce= 0.8f;
	
	float timeSinceSpawn;
	float moveDelay = 0.3f; 
	bool hasPlayedSound;
	bool collisionTest;


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


		if (timeSinceSpawn > moveDelay){

			rigidBody.AddForce(directionToMothership*moveForce);

			if (hasPlayedSound == false){ 
//				GetComponent<AudioSource>().Play();
				hasPlayedSound = true;
			}
		}

//		if (OnCollisionEnter2D) {
//			anim.SetTrigger ("explode");
//		}


	}


	void OnCollisionEnter2D (Collision2D thisCollision){
//		GameLogic gameLogic = game.GetComponent<GameLogic>(); 
//		gameLogic.EnemyTouch (gameObject);
		//anim.SetTrigger ("explode");

		if(thisCollision.gameObject.tag == "Bullet")
		{
			collisionTest = true;
			//anim.SetTrigger ("explode");
			//Example ();
			//Destroy(this.gameObject);

		}

		if (collisionTest) {
			anim.SetTrigger ("explode");
			DeathAnimationTimer();
		}

	}

//	IEnumerator DeathAnimation() {
//			if(collisionTest){
//			anim.SetTrigger ("explode");
//			yield return new WaitForSeconds(0.2f);
//			Destroy(gameObject);
//		}
//	}

	void DestroyObject(){
		Destroy (gameObject);
	}

	void DeathAnimationTimer(){
	
		Invoke ("DestroyObject", 0.2f);
	}

}


