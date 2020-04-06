using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitatController : MonoBehaviour {

    //VARIABLES

	[Header("Health Attributes")]
	public int startingHealth;
	private int currentHealth;

	[Header("Destruction Attributes")]
	public GameObject intactModel;
	public GameObject destroyedModel;
	public GameObject shatterEffectPrefab;

	//Destruction state
	private bool isDestroyed = false;
	public bool IsDestroyed { get => isDestroyed; }

    //METHODS
    
	//Setup the controller
	public void InitialiseController () {
		currentHealth = startingHealth;
	}

	//Get the habitats current health as a percentage
	public float GetHealthPercentage() {
		return (float)currentHealth / startingHealth;
	}

	//Damage the habitat and change out the model if destoryed
	public bool Damage (int amount) {
		currentHealth -= amount;

		if (currentHealth <= 0) {
			//Switch to destroyed model
			intactModel.SetActive(false);
			destroyedModel.SetActive(true);

			GameObject shatterClone = Instantiate(shatterEffectPrefab, transform.position, Quaternion.identity);
			Destroy(shatterClone, 5f);
			
			//Return true only if this damage killed it (habitat can still be damaged after destruction)
			if (isDestroyed == true) {
				return false;
			} else {
				isDestroyed = true;
				return true;
			}
		} else {
			return false;
		}
	}	
    
}
