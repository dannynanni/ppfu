using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using InControl;

public class HighScoreManager : MonoBehaviour {

	public class Entry {
		public string name;
		public int score;

		public Entry(string newName, int newScore) {
			name = newName;
			score = newScore;
		}

		public string GetEntry() {
			if(score != 0) {
				return string.Concat(name, " ", score.ToString());
			}
			else {
				return string.Concat(name, " ----");
			}
		}

		public string scoreVal() {
			return score.ToString();
		}
	}

	public Text HighScoreDisplay;
	public int numEntries = 10;
	public List<Entry> entries;
	private string HighScoreHeader;
	private string nameEntry;
	private int scoreEntry;
	bool enteringScore;
	private char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
	private int charPosition = 0;

	// Use this for initialization
	void Awake () {
		//ClearLeaderBoard ();
		enteringScore = false;
		HighScoreHeader = SceneManager.GetActiveScene().name + "HighScore";
		entries = new List<Entry>();
		string key;
		for(int i = 0; i < numEntries; i++) {
			key = HighScoreHeader + i.ToString();
			if(PlayerPrefs.HasKey(key + "score")) {
				entries.Add(new Entry(PlayerPrefs.GetString(key + "name"), PlayerPrefs.GetInt(key + "score")));
			}
			else {
				entries.Add(new Entry(" ", 0));
			}
		}
	}

	void Update() {
		if(enteringScore) {
			if(Input.GetKeyDown(KeyCode.Return)) {
				CreateNewEntry(nameEntry, scoreEntry);
				enteringScore = false;
			}
			else {
				HighScoreDisplay.text = "ENTER YOUR NAME\n " + nameEntry + alphabet[charPosition].ToString();
				HighScoreDisplay.alignment = TextAnchor.MiddleCenter;
				// This forces the stick to return to neutral
				if(!InputManager.ActiveDevice.LeftStick.LastState) {
					if(InputManager.ActiveDevice.LeftStick.X > 0) {
						charPosition += 1;
						if(charPosition >= alphabet.Length) {
							charPosition = 0;
						}
					}
					else if(InputManager.ActiveDevice.LeftStick.X < 0) {
						charPosition -= 1;
						if(charPosition < 0) {
							charPosition = alphabet.Length - 1;
						}
					}
					if(InputManager.ActiveDevice.Action1.WasPressed) {
						nameEntry = string.Concat(nameEntry, alphabet[charPosition].ToString());
						if(nameEntry.Length > 2) {
							CreateNewEntry(nameEntry, scoreEntry);
							enteringScore = false;
							PrintScores();
						}
					}
				}
			}
		}
	}

	public void SetScores() {
		string key;
		for(int i = 0; i < numEntries; i++) {
			key = HighScoreHeader + i.ToString();
			PlayerPrefs.SetString(key + "name", entries[i].name);
			PlayerPrefs.SetInt(key + "score", entries[i].score);
		}
	}

	public void PrintScores() {
		string scoreList = "";
		foreach(Entry entry in entries) {
			scoreList = string.Concat(scoreList, entry.GetEntry(), "\n");
		}
		HighScoreDisplay.text = scoreList;
		HighScoreDisplay.alignment = TextAnchor.MiddleCenter;
	}

	public void ClearLeaderBoard() {
		string key;
		for(int i = 0; i < numEntries; i++) {
			key = HighScoreHeader + i.ToString();
			PlayerPrefs.DeleteKey(key + "name");
			PlayerPrefs.DeleteKey(key + "score");
		}
	}

	void OnApplicationQuit()
	{
		PlayerPrefs.Save();
	}
	
	public bool MadeHighScoreList(int score) {
	Debug.Log(entries);
	Debug.Log(numEntries);
		return score > entries[numEntries -1].score;
	}

	public void CreateNewEntry(string name, int score) {
		Entry newChallenger = new Entry(name, score);
		for(int i = 0; i < entries.Count; i++) {
			if(entries[i].score < newChallenger.score) {
				entries.Insert(i, newChallenger);
				break;
			}
		}
		SetScores();
		entries.Remove(entries[numEntries]);
	}

	public void InputNewName(int score) {
		enteringScore = true;
		scoreEntry = score;
	}

	IEnumerator DisplayAndReset() {
		PrintScores();
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
