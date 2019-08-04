using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitatController : MonoBehaviour {

    //VARIABLES

	public int startingHealth;
	private int currentHealth;

	public GameObject intactModel;
	public GameObject destroyedModel;
	public GameObject shatterEffectPrefab;

	private bool isDestroyed = false;
	public bool IsDestroyed { get => isDestroyed; }

    //METHODS
    
	public void InitialiseController () {
		currentHealth = startingHealth;
	}

	public float GetHealthPercentage() {
		return (float)currentHealth / startingHealth;
	}

	public bool Damage (int amount) {
		currentHealth -= amount;

		if (currentHealth <= 0) {
			intactModel.SetActive(false);
			destroyedModel.SetActive(true);

			GameObject shatterClone = Instantiate(shatterEffectPrefab, transform.position, Quaternion.identity);
			Destroy(shatterClone, 5f);
			
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
