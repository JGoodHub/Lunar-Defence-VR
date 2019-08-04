using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitatManager : MonoBehaviour {

    //SINGLETON PATTERN

    public static HabitatManager instance = null;
	
	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
		}
	}

    //VARIABLES

	public HabitatController[] buildings;

    //METHODS

	public void InitialiseManager () {
		foreach (HabitatController hab in buildings) {
			hab.InitialiseController();
		}
	}

	public HabitatController GetRandomBuilding () {
		if (buildings.Length > 0) {
			int buildingIndex = Random.Range(0, buildings.Length);
			return buildings[buildingIndex];
		} else {
			Debug.LogError("ERROR: No buildings inn the buildings array");
			return null;
		}
	}
    
    
    
}
