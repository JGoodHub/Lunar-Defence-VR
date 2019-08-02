﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    //SINGLETON PATTERN

    public static BuildingManager instance = null;
	
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