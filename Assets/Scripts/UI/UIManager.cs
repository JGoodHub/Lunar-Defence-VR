using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    //SINGLETON PATTERN

    public static UIManager instance = null;
	
	void Awake () {
		if (instance == null) {
			instance = this;
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

    //METHODS

	public void InitialiseManager () {
		UpdateCurrentScores();

		for (int i = 0; i < healthBarContainers.Length; i++) {
			HideHealthForHabitat(i);
		}
	}

	void Update () {
		UpdateTargeterPosition();	
	}
    
	private void UpdateTargeterPosition () {
		if (TurretManager.instance.TargetMeteor == null) {
			targeterTransform.position = new Vector3(0, -100, 0);
		} else {
			Vector3 targetDirection = TurretManager.instance.TargetMeteor.transform.position - Camera.main.transform.position;		
			targeterTransform.position = Camera.main.transform.position + (targetDirection.normalized * 5f);
			targeterTransform.forward = Camera.main.transform.forward;

			//TODO ---> Have the targeter resize based on distance to the meteor
		}
	}

	public void DisplayHealthForHabitat (int habID) {
		healthBarContainers[habID].SetActive(true);
		healthBarFills[habID].fillAmount = HabitatManager.instance.buildings[habID].GetHealthPercentage();
	}

	public void HideHealthForHabitat (int habID) {
		healthBarContainers[habID].SetActive(false);
	}

	public void UpdateCurrentScores () {
		//TODO ---> Replace with string builder for better efficiency
		string output = "";

		output += "Best: " + ScoreManager.instance.LocalHighscore + "\n";
		output += "------------------\n";
		output += "Score: " + ScoreManager.instance.CurrentScore + "\n";
		output += "Kills: " + ScoreManager.instance.MeteorsDestroyed;

		scoreBoardText.SetText(output);
	}
    
    
}
