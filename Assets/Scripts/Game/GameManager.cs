using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //METHODS

	void Start () {
		TurretManager.instance.InitialiseManager();
		MeteorManager.instance.InitialiseManager();
		BuildingManager.instance.InitialiseManager();
	}
    
    
}
