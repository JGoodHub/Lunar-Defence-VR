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

	[HideInInspector] public bool gameOver = false;

    //METHODS

	void Start () {
		TurretManager.instance.InitialiseManager();
		MeteorManager.instance.InitialiseManager();
		HabitatManager.instance.InitialiseManager();
		ScoreManager.instance.InitialiseManager();
		UIManager.instance.InitialiseManager();
	}

	public void EndGame () {
		gameOver = true;
		UIManager.instance.ShowGameOverPanel();
		ScoreManager.instance.SaveLocalHighscore();
	}
	
	public void RestartGame () {
		Debug.Log("Loading Scene");
		SceneManager.LoadScene(0);
	}
    
    
}
