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

	[Header("Habitat Collection Attribute")]
	public HabitatController[] habitats;
	private int habitatsRemaining;

    //METHODS

	//Setup the manager attributes
	public void InitialiseManager () {
		habitatsRemaining = habitats.Length;
		foreach (HabitatController hab in habitats) {
			hab.InitialiseController();
		}
	}

	//Get a random habitat from the collection, intact or not
	public HabitatController GetRandomHabitat () {
		if (habitats.Length > 0) {
			int buildingIndex = Random.Range(0, habitats.Length);
			return habitats[buildingIndex];
		} else {
			Debug.LogError("ERROR: No buildings in the buildings array");
			return null;
		}
	}

	//Decrease the number of habitats remaining, if zero trigger game over
	public void DecrementHabitatsRemaining () {
		habitatsRemaining--;

		if (habitatsRemaining <= 0) {
			GameManager.instance.EndGame();
		}
	}
    
}
