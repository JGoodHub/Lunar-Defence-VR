using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    //METHODS

	void Update () {
		UpdateTargeterPosition();
	}
    
	private void UpdateTargeterPosition () {
		if (TurretManager.instance.targetMeteor == null) {
			targeterTransform.position = new Vector3(0, -100, 0);
		} else {
			Vector3 targetDirection = TurretManager.instance.targetMeteor.transform.position - Camera.main.transform.position;		
			targeterTransform.position = Camera.main.transform.position + (targetDirection.normalized * 5f);
			targeterTransform.forward = Camera.main.transform.forward;

			//TODO ---> Have the targeter resize based on distance to the meteor
		}
	}
    
    
}
