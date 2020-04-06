using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    
    //SINGLETON PATTERN

    public static ScoreManager singleton = null;
	
	void Awake () {
		if (singleton == null) {
			singleton = this;
		} else {
			Destroy(gameObject);
		}
	}

    //VARIABLES

	//Local highscore attributes
	private int localHighscore;
	public int LocalHighscore { get => localHighscore; }

	//Current score attributes
	private int currentScore;
	public int CurrentScore { get => currentScore; }

	//Kill count attributes
	private int meteorsDestroyed;
	public int MeteorsDestroyed { get => meteorsDestroyed; }

    //METHODS

	//Setup the manager
	public void InitialiseManager () {
		currentScore = 0;
		meteorsDestroyed = 0;

		LoadLocalHighscore();
	}

	//Load the local highscore from storage into memory
    private void LoadLocalHighscore() {
		localHighscore = PlayerPrefs.GetInt("lunar_highscore", 0);
    }

	//Save the current highscore into storage
	public void SaveLocalHighscore () {
		PlayerPrefs.SetInt("lunar_highscore", localHighscore);
	}

	//Increase the player score and possible the highscore
	public void IncreaseScore (int amount) {
		currentScore += amount;

		if (currentScore > localHighscore) {
			localHighscore = currentScore;
		}
	}	

	//Increase the kill count by one
	public void IncrementMeteorsDestroyed () {
		meteorsDestroyed++;
	}

}
