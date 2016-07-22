using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OLDGameLogic : MonoBehaviour {

	public GameObject player;
	public GameObject player2;
	public GameObject mothership;
	public int score;
	int highscore;
	bool gameStarted ;

//	public GameObject fireMessage;
//	int fireMessageLife;
//	Vector3 fireMessagePosition;
	public Text scoreText;
	public Text highScoreText;
	GameObject fireThing;
	//public AudioSource messageSignal;
	public GameObject enemyPrefab;
	public Transform enemyContainer; 

	public int numberOfPlayers;
	float timeSinceLastEnemyCreated = 0;

	float initialEnemySpawnDelay = 1f;
	float currentEnemySpawnDelay;

	public GameObject boss; 
	public bool bossAlive = false;

	//camera zoom variables
	public float camSize = 14f;
	public float camSizeLimit = 20f;
	public float timeLerp = 3;
	public float timeLerpValue;
	public float increment;

	void Start () {
		highscore = 0;
		Reset();
		//Camera.main.orthographicSize = true;
		StartCoroutine("SpawnBossTimer");
	}
	

	void Update () {
		//highScoreText.text = "" + highscore;  
		if (gameStarted) {
			scoreText.text = "" + score;

			if (numberOfPlayers == 1) {
				timeSinceLastEnemyCreated += Time.deltaTime; 
				if (timeSinceLastEnemyCreated > currentEnemySpawnDelay) { 
					Vector2 newEnemyPosition;
					float scaleX;
					float scaleY;
					Collider2D overlappingCollider;
					do {
						float enemySpawnDistance = Random.Range (27f, 42f);
						float enemySpawnAngle = Random.Range (-Mathf.PI, Mathf.PI);
						scaleX = Random.Range (1, 1.8f);
						scaleY = Random.Range (1, 1.8f);

						newEnemyPosition = mothership.transform.position;

						newEnemyPosition += new Vector2 (Mathf.Cos (enemySpawnAngle), Mathf.Sin (enemySpawnAngle)) * enemySpawnDistance;

						overlappingCollider = Physics2D.OverlapCircle (newEnemyPosition, Mathf.Sqrt (Mathf.Pow (scaleX * 2.5f, 2) + Mathf.Pow (scaleY * 3.65f, 2)));
						Debug.Log (overlappingCollider);
					} while(overlappingCollider != null);

					GameObject newEnemy = Instantiate (enemyPrefab, newEnemyPosition, Quaternion.identity) as GameObject;
					newEnemy.transform.parent = enemyContainer;
					newEnemy.transform.localScale = new Vector3 (scaleX, scaleY, 0f);

					timeSinceLastEnemyCreated = 0; 
					currentEnemySpawnDelay *= 0.99991f;
					if (currentEnemySpawnDelay < 0.5f)
						currentEnemySpawnDelay = 0.5f;
				}
			}

			if (numberOfPlayers == 2) {
				timeSinceLastEnemyCreated += Time.deltaTime; 
				if (timeSinceLastEnemyCreated > currentEnemySpawnDelay) { 
					Vector2 newEnemyPosition;
					float scaleX;
					float scaleY;
					Collider2D overlappingCollider;
					do {
						float enemySpawnDistance = Random.Range (27f, 42f);
						float enemySpawnAngle = Random.Range (-Mathf.PI, Mathf.PI);
						scaleX = Random.Range (1, 1.8f);
						scaleY = Random.Range (1, 1.8f);
						
						newEnemyPosition = mothership.transform.position;
						
						newEnemyPosition += new Vector2 (Mathf.Cos (enemySpawnAngle), Mathf.Sin (enemySpawnAngle)) * enemySpawnDistance;
						
						overlappingCollider = Physics2D.OverlapCircle (newEnemyPosition, Mathf.Sqrt (Mathf.Pow (scaleX * 2.5f, 2) + Mathf.Pow (scaleY * 3.65f, 2)));
						Debug.Log (overlappingCollider);
					} while(overlappingCollider != null);
					
					GameObject newEnemy = Instantiate (enemyPrefab, newEnemyPosition, Quaternion.identity) as GameObject;
					newEnemy.transform.parent = enemyContainer;
					newEnemy.transform.localScale = new Vector3 (scaleX, scaleY, 0f);
					
					timeSinceLastEnemyCreated = 0; 
					currentEnemySpawnDelay *= 0.1f;
					if (currentEnemySpawnDelay < 0.5f)
						currentEnemySpawnDelay = 0.5f;
				}
			}


			camSize = Camera.main.orthographicSize;
			timeLerpValue = timeLerp * Time.deltaTime;

			if (bossAlive) {
				Debug.Log ("zooming in");
				//camSize = Mathf.Lerp (Camera.main.orthographicSize, Camera.main.orthographicSize + increment, timeLerpValue);
				Camera.main.orthographicSize = Mathf.Lerp(14f, 20f, 300f * Time.deltaTime);
			}


				


			}
				
		}
			


	void SpawnBoss (){
		
		//Vector3 randomBossPos = new Vector3 (Random.Range(-40f, 40f), Random.Range(-30f, 30f), 0);

		Vector2 newBossPosition;
		float enemyBossDistance = Random.Range (27f, 42f);
		float enemyBossAngle = Random.Range (-Mathf.PI, Mathf.PI);
		newBossPosition = mothership.transform.position;
		newBossPosition += new Vector2 (Mathf.Cos (enemyBossAngle), Mathf.Sin (enemyBossAngle)) * enemyBossDistance;
		Instantiate (boss, newBossPosition, Quaternion.identity);
		bossAlive = true;
		Debug.Log ("SpawnBoss");
		
	}

//	void SpawnBossTimer() {
//		Debug.Log ("SpawnBossTimer");
//		InvokeRepeating("SpawnBoss", 1, 2f);
//	}

	IEnumerator SpawnBossTimer () {
		while (true) {
			SpawnBoss ();
			Debug.Log ("coroutine");
			yield return new WaitForSeconds(30f);
			SpawnBoss();
		
	}

			
	

//	void Message ()
//	{ 
//			fireMessageLife = 2;
//			GameObject FireMessage = (GameObject)
//				Instantiate (fireMessage, fireMessagePosition,
//				             transform.rotation);
//			Debug.Log ("fireMessage");
//			messageSignal.Play ();
//			
//			Destroy (FireMessage, 5f);
//	}}
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


		PlayerControl pControl = player.GetComponent<PlayerControl>();
		pControl.Reset();

		if (player2 != null) {
			PlayerControlPlayer2 pControl2 = player2.GetComponent<PlayerControlPlayer2> ();
			pControl2.Reset ();
		}

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

		//fireMessageLife = 1;

	}
	

	public void EnemyTouch(GameObject sender){

		Destroy(sender);

		//score += 1;

		mothership.transform.localScale = new Vector3(0.2f, 0.2f, 1f); 

	}

	//"Growing up means watching my heroes turn human in front of me
	//The songs we wrote at eighteen seem shortsighted and naïve
	//So when the weather breaks, I'll pull my hoodie up over my face
	//I won't run away, run away
	//As fucked as this place got, it made me me"

	//Thanks to Ben Sironko for help with the Overlap Area code.

	//Made by Danny Nanni, originally part of Game Studio with Bennett Foddy, MFA at NYU GameCenter, Fall 2015

 }
