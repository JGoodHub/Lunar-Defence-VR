using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //SINGLETON PATTERN

    public static GameManager instance = null;
	
	void Awake () {
		if (instance == null) {
			instance = this;
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
		TurretManager.instance.InitialiseManager();
		MeteorManager.instance.InitialiseManager();
		HabitatManager.instance.InitialiseManager();
		ScoreManager.instance.InitialiseManager();
		UIManager.instance.InitialiseManager();
	}

	//Trigger the end of the game
	public void EndGame () {
		gameOver = true;
		UIManager.instance.ShowGameOverPanel();
		ScoreManager.instance.SaveLocalHighscore();
	}
	
	//Restart the game
	public void RestartGame () {
		SceneManager.LoadScene(0);
	}   
    
}
