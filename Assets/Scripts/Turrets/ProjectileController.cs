using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO ---> Migrate projectile and meteor to superclass

public class ProjectileController : MonoBehaviour, IPoolObject {

    //VARIABLES

    public float startingSpeed;
	private float currentSpeed;

	private Collider projectileCollider;

    //METHODS

	public void Initialise () {
		projectileCollider = GetComponent<BoxCollider>();
	}

	void Update () {
		transform.position += (transform.forward * currentSpeed) * Time.deltaTime;

		if (transform.position.y >= MeteorManager.instance.spawnCeiling) {
			PassivateObject();
		}
	}
	
	public void ActivateObject() {
		currentSpeed = startingSpeed;
		projectileCollider.enabled = true;

		TurretManager.instance.ActivateProjectiles.Add(this);
		TurretManager.instance.PassivatedProjectiles.Remove(this);
    }

    public void PassivateObject() {
		currentSpeed = 0;
		projectileCollider.enabled = false;
		transform.position = new Vector3(0, -900, 0);

		TurretManager.instance.ActivateProjectiles.Remove(this);
		TurretManager.instance.PassivatedProjectiles.Add(this);
    }

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Meteor")) {
			PassivateObject();
		}
	}
    
    
    
}
