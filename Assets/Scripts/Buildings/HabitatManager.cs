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
	private int habitatsRemaining;

    //METHODS

	public void InitialiseManager () {
		habitatsRemaining = buildings.Length;
		foreach (HabitatController hab in buildings) {
			hab.InitialiseController();
		}
	}

	public HabitatController GetRandomBuilding () {
		if (buildings.Length > 0) {
			int buildingIndex = Random.Range(0, buildings.Length);
			return buildings[buildingIndex];
		} else {
			Debug.LogError("ERROR: No buildings in the buildings array");
			return null;
		}
	}

	public void DecrementHabitatsRemaining () {
		Debug.Log("Hit");

		habitatsRemaining--;

		if (habitatsRemaining <= 0) {
			Debug.Log("Game Over");
			GameManager.instance.EndGame();
		}
	}
    
    
    
}
