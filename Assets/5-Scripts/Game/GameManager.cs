using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //SINGLETON PATTERN

    public static GameManager singleton = null;
	
	void Awake () {
		if (singleton == null) {
			singleton = this;
		} else {
			Destroy(gameObject);
		}
	}

    //VARIABLES

	//Game over state
	[HideInInspector] public bool gameOver = false;

    //METHODS

	//Initialise all the managers in the correct order
	void Start () {
		TurretManager.singleton.InitialiseManager();
		MeteorManager.singleton.InitialiseManager();
		HabitatManager.singleton.InitialiseManager();
		ScoreManager.singleton.InitialiseManager();
		UIManager.singleton.InitialiseManager();
	}

	//Trigger the start of the game
	public void StartGame () {
		MeteorManager.singleton.StartSpawningMeteors();
	}

	//Trigger the end of the game
	public void EndGame () {
		gameOver = true;
		UIManager.singleton.ShowGameOverPanel();
		ScoreManager.singleton.SaveLocalHighscore();
	}
	
	//Restart the game
	public void RestartGame () {
		SceneManager.LoadScene(0);
	}   
    
}
