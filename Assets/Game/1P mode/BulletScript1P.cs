using UnityEngine;
using System.Collections;

public class BulletScript1P : MonoBehaviour {

	private float moveSpeed = 30f; 
	private float timeSpentAlive; 
	private GameObject player;
	private GameObject player2;
	GameLogic gameLogic; 
	GameObject game; 
	public AudioSource cometDeath;
	GameObject sound;
	AudioSource cometSound;


	void Start () {
	
		player = GameObject.Find ("player");
		player2 = GameObject.Find ("player2");
		game = GameObject.Find("GameManager");
		gameLogic = game.GetComponent<GameLogic>();

		sound = GameObject.Find ("cometDeathSound");
		cometSound = sound.GetComponent<AudioSource> ();

	}
	

	void Update () {

		timeSpentAlive += Time.deltaTime;
		if (timeSpentAlive > 1) 
		{
			removeMe();
		}
		// move the bullet
		Vector3 newPosition = transform.position; //get a copy of the current position
		newPosition += transform.up * moveSpeed * Time.deltaTime; //move move it
		newPosition.z = 0;
		GetComponent<Rigidbody2D> ().MovePosition (newPosition); //move the physics body
					
	
	}
	void removeMe () {

		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D Other) {



	if (Other.gameObject.tag == "Enemy") {
			//Destroy (Other.gameObject); //destroy the object I hit
			Destroy (gameObject); //destroy this bullet
			gameLogic.score += 1;
			cometSound.Play();


		}




	if (Other.gameObject.GetComponent( typeof(PlayerControl2) ) != null &&
		Other.gameObject != player ) 
		{
			removeMe();
		}

		if (Other.gameObject.GetComponent( typeof(PlayerControlPlayer2) ) != null &&
		    Other.gameObject != player ) 
		{
			removeMe();
		}
			removeMe(); 
		}
	
	
	}
