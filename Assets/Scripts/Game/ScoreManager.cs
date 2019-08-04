using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    
    //SINGLETON PATTERN

    public static ScoreManager instance = null;
	
	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
		}
	}

    //VARIABLES

	private int localHighscore;
	public int LocalHighscore { get => localHighscore; }

	private int currentScore;
	public int CurrentScore { get => currentScore; }

	private int meteorsDestroyed;
	public int MeteorsDestroyed { get => meteorsDestroyed; }

    //METHODS

	public void InitialiseManager () {
		currentScore = 0;
		meteorsDestroyed = 0;

		LoadLocalHighscore();
	}

    private void LoadLocalHighscore() {
		localHighscore = PlayerPrefs.GetInt("lunar_highscore", 0);
    }

	public void SaveLocalHighscore () {
		PlayerPrefs.SetInt("lunar_highscore", localHighscore);
	}

	public void IncreaseScore (int amount) {
		currentScore += amount;

		if (currentScore > localHighscore) {
			localHighscore = currentScore;
		}
	}	

	public void IncrementMeteorsDestroyed () {
		meteorsDestroyed++;
	}

}
