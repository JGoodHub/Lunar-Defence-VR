using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    //SINGLETON PATTERN

    public static UIManager singleton = null;
	
	void Awake () {
		if (singleton == null) {
			singleton = this;
		} else {
			Destroy(gameObject);
		}
	}

    //VARIABLES

	[Header("Targeting Elements")]
	public RectTransform targeterTransform;

	[Header("Habitat Health Elements")]
	public GameObject[] healthBarContainers;
	public Image[] healthBarFills;

	[Header("Score Board Elements")]
	public TextMeshProUGUI scoreBoardText;

	[Header("Game Over Panel Elements")]
	public GameObject gameOverPanel;

    //METHODS

	//Setup the managers attributes
	public void InitialiseManager () {
		UpdateCurrentScores();

		for (int i = 0; i < healthBarContainers.Length; i++) {
			HideHealthForHabitat(i);
		}
	}

	//Called once a frame
	void Update () {
		UpdateTargeterPosition();	
	}

	//Trigger the game to restart
	public void RestartGameTrigger () {
		GameManager.singleton.RestartGame();
	}

	//Unhide the game over panel
	public void ShowGameOverPanel () {
		gameOverPanel.SetActive(true);
	}
    
	//Update the targeters position to hover over the target meteor
	private void UpdateTargeterPosition () {
		if (TurretManager.singleton.TargetMeteor == null) {
			targeterTransform.position = new Vector3(0, -100, 0);
		} else {
			Vector3 targetDirection = TurretManager.singleton.TargetMeteor.transform.position - Camera.main.transform.position;		
			targeterTransform.position = Camera.main.transform.position + (targetDirection.normalized * 5f);
			targeterTransform.forward = Camera.main.transform.forward;

			//TODO ---> Have the targeter resize based on distance to the meteor
		}
	}

	//Display the health overlay for a given habitat
	public void DisplayHealthForHabitat (int habID) {
		healthBarContainers[habID].SetActive(true);
		healthBarFills[habID].fillAmount = HabitatManager.singleton.habitats[habID].GetHealthPercentage();
	}

	//Hide the health overlay for a given habitat
	public void HideHealthForHabitat (int habID) {
		healthBarContainers[habID].SetActive(false);
	}

	//Create the string for the score board and display it
	public void UpdateCurrentScores () {
		//TODO ---> Replace with string builder for better efficiency
		string output = "";

		output += "Best: " + ScoreManager.singleton.LocalHighscore + "\n";
		output += "------------------\n";
		output += "Score: " + ScoreManager.singleton.CurrentScore + "\n";
		output += "Kills: " + ScoreManager.singleton.MeteorsDestroyed;

		scoreBoardText.SetText(output);
	}
    
    
}
