using UnityEngine;
using System.Collections;

public class MothershipScript : MonoBehaviour {

	public AudioSource gameOverSound;

	public GameObject particle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D thisCollision){
		if (thisCollision.gameObject.tag == "Enemy") {
			Debug.Log ("gameoverload");
			Instantiate (particle, this.transform.position, this.transform.rotation);
			gameOverSound.Play ();
			GetComponent<ShakeCamera> ().BeginContact (Vector2.zero);
			GetComponent<SpriteRenderer>().enabled = false;
			Destroy (thisCollision.gameObject);
			LoadGameOver();
		}

		if (thisCollision.gameObject.tag == "Boss") {
			Debug.Log ("gameoverload");
			Instantiate (particle, this.transform.position, this.transform.rotation);
			gameOverSound.Play ();
			GetComponent<ShakeCamera> ().BeginContact (Vector2.zero);
			GetComponent<SpriteRenderer>().enabled = false;
			Destroy (thisCollision.gameObject);
			LoadGameOver();
		}
	}

	void GameOverScreen(){
		Application.LoadLevel (3);
	}
	
	void LoadGameOver(){
		Invoke ("GameOverScreen", 0.5f);
	}
}
