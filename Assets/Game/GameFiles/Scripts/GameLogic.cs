using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {
	
	//player variables
	public GameObject redShip;
	public GameObject yellowShip;

	//planet in the center
	public GameObject planet;

	//score variables
	public int score;
	public Text scoreText;
	int highScore;
	public Text highScoreText;

	//enemy variables
	public GameObject enemyPrefab;
	public Transform enemyContainer;
	public float timeSinceLastEnemyCreated = 0;
	public float initialEnemySpawnDelay = 1f;
	public float currentEnemySpawnDelay;
	public GameObject boss; 
	public bool bossAlive = false;

	//general variables
	public bool gameStarted;
	//for camera zoom functions
	public float zoomLerpSpeed;



	// Use this for initialization
	void Start () {
		highScore = 0;
		Reset ();
		StartCoroutine ("SpawnBossTimer");
	}
	
	// Update is called once per frame
	void Update () {

		if (gameStarted) {
			
			//creating the enemy behavior
			//using OverlapCircle to make sure two enemies don't spawn on top of each other
			//Making their start position relative to the position of the planet with the vectors of Angle and Distance
			//The enemies "gravitate" at a move speed in (in EnemyScript) toward the planet
			//They spawn randomly within an area of 27 - 42

			scoreText.text = "" + score;
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

					newEnemyPosition = planet.transform.position;
					newEnemyPosition += new Vector2 (Mathf.Cos (enemySpawnAngle), Mathf.Sin (enemySpawnAngle)) * enemySpawnDistance;

					overlappingCollider = Physics2D.OverlapCircle (newEnemyPosition, Mathf.Sqrt (Mathf.Pow (scaleX * 2.5f, 2) + Mathf.Pow (scaleY * 3.65f, 2)));
					Debug.Log (overlappingCollider);
				} while(overlappingCollider != null);

				GameObject newEnemy = Instantiate (enemyPrefab, newEnemyPosition, Quaternion.identity) as GameObject;
				newEnemy.transform.parent = enemyContainer;
				newEnemy.transform.localScale = new Vector3 (scaleX, scaleY, 0f);

				timeSinceLastEnemyCreated = 0; 
				currentEnemySpawnDelay *= 1f;
				if (currentEnemySpawnDelay < 0.5f)
					currentEnemySpawnDelay = 0.5f;
		}
	
	}

	}

	void SpawnBoss (){


		Vector2 newBossPosition;
		float enemyBossDistance = Random.Range (27f, 42f);
		float enemyBossAngle = Random.Range (-Mathf.PI, Mathf.PI);
		newBossPosition = planet.transform.position;
		newBossPosition += new Vector2 (Mathf.Cos (enemyBossAngle), Mathf.Sin (enemyBossAngle)) * enemyBossDistance;
		Instantiate (boss, newBossPosition, Quaternion.identity);
		bossAlive = true;
		StartZoomOut ();
		Debug.Log ("SpawnBoss");

	}

	IEnumerator SpawnBossTimer () {
		while (true) {
			Debug.Log ("SpawnBossTimer");
			yield return new WaitForSeconds(20f);
			SpawnBoss();
		}
	}

	public void StartGame(){
		gameStarted = true;
	}

	public IEnumerator ZoomOut() {
			float percentage = 0.0f;
			while (percentage < 1.0f) {
				percentage = Mathf.Min (percentage + zoomLerpSpeed * Time.deltaTime, 1.0f);
				Camera.main.orthographicSize = Mathf.Lerp (14f, 20f, percentage);
				yield return 0;
				Debug.Log ("ZoomingOut");
			}
		} 

	public IEnumerator ZoomIn() {
		float percentage = 0.0f;
		while(percentage < 1.0f) {
			percentage = Mathf.Min(percentage + zoomLerpSpeed * Time.deltaTime, 1.0f);
			Camera.main.orthographicSize = Mathf.Lerp(20f, 14f, percentage);
			yield return 0;
			Debug.Log ("ZoomingIn");
		}
	}

	public void StartZoomIn() {
		StartCoroutine("ZoomIn");
	}

	public void StartZoomOut(){
	
		StartCoroutine ("ZoomOut");
	}

	public void Reset() {
		currentEnemySpawnDelay = initialEnemySpawnDelay;
		gameStarted = false;

		for(int i = 0; i < enemyContainer.childCount; i++){
			Transform enemy = enemyContainer.GetChild(i);
			Destroy(enemy.gameObject);
		}

		if (score > highScore){
			highScore = score; 
		}

		score = 0;
		timeSinceLastEnemyCreated = 0f;

		GetComponent<AudioSource>().Play();

		gameStarted = true;
	}
}
