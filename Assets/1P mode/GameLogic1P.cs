using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameLogic1P : MonoBehaviour {

	public GameObject player;
	public GameObject player2;
	public GameObject mothership;
	public int score;
	int highscore;
	bool gameStarted ;

	public Text scoreText;
	public Text highScoreText;
	
	public GameObject enemyPrefab;
	public Transform enemyContainer; 

	float timeSinceLastEnemyCreated = 0;

	float initialEnemySpawnDelay = 1.5f;
	float currentEnemySpawnDelay;

	void Start () {
		highscore = 0;
		Reset();
	}
	

	void Update () {
		highScoreText.text = "" + highscore;  
		if (gameStarted){
			scoreText.text = "" + score;

			timeSinceLastEnemyCreated += Time.deltaTime; 
			//Debug.Log (timeSinceLastEnemyCreated);
			if (timeSinceLastEnemyCreated > currentEnemySpawnDelay){ 

				float enemySpawnDistance = 27.0f; 


				float enemySpawnAngle = Random.Range(-Mathf.PI, Mathf.PI);


				Vector2 newEnemyPosition = mothership.transform.position;

				newEnemyPosition.x += enemySpawnDistance * Mathf.Cos(enemySpawnAngle);
				newEnemyPosition.y += enemySpawnDistance * Mathf.Sin(enemySpawnAngle);


				GameObject newEnemy = Instantiate(enemyPrefab,newEnemyPosition,Quaternion.identity) as GameObject;
				newEnemy.transform.parent = enemyContainer;
				newEnemy.transform.localScale = new Vector3 ( Random.Range (1,1.8f), Random.Range (1,1.8f), 0f);

				timeSinceLastEnemyCreated = 0; 
				currentEnemySpawnDelay *= 0.94f;
				if (currentEnemySpawnDelay < 0.5f) currentEnemySpawnDelay = 0.5f;
			}
		} else {
			scoreText.text = "shoot the comets!";
		}

	

	}
	
	public void StartGame(){
		gameStarted = true;
	}

	public void Reset() {
		currentEnemySpawnDelay = initialEnemySpawnDelay;
		gameStarted = false;

		//Debug.Log ("Resetting!");
		for(int i = 0; i < enemyContainer.childCount; i++){
			Transform enemy = enemyContainer.GetChild(i);
		  	Destroy(enemy.gameObject);
		}


		PlayerControl2 pControl = player.GetComponent<PlayerControl2>();
		pControl.Reset();

		PlayerControlPlayer2 pControl2 = player2.GetComponent<PlayerControlPlayer2> ();
		pControl2.Reset ();

		mothership.transform.position = Vector3.zero;

		Rigidbody2D mothershipBody = mothership.GetComponent<Rigidbody2D>();
		mothershipBody.velocity = Vector2.zero;

		if (score > highscore){
			highscore = score; 
		}

		score = 0;
		timeSinceLastEnemyCreated = 0f;


		GetComponent<AudioSource>().Play();

		gameStarted = true;
	}


	public void EnemyTouch(GameObject sender){

		Destroy(sender);

		//score += 1;

		mothership.transform.localScale = new Vector3(0.2f, 0.2f, 1f); 

	}
}
