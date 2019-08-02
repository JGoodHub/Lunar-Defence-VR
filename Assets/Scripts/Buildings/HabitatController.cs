using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitatController : MonoBehaviour {

    //VARIABLES

	public int startingHealth;
	private int currentHealth;

    //METHODS
    
	public void InitialiseController () {
		currentHealth = startingHealth;
	}

	public void Log () {
		Debug.Log("Message");
	}
    
}
