using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO ---> Migrate projectile and meteor to superclass

public class ProjectileController : MonoBehaviour, IPoolObject {

    //VARIABLES

	[Header("Projectiles Speed and Death Attributes")]
    public float startingSpeed;
	private float currentSpeed;
	public GameObject explosionPrefab;

	private Collider projectileCollider;

    //METHODS

	//Setup the controllers attributes
	public void InitialiseController () {
		projectileCollider = GetComponent<BoxCollider>();
	}

	//Move the projectile forward, passivate it if it flies off map
	void Update () {
		transform.position += (transform.forward * currentSpeed) * Time.deltaTime;

		if (transform.position.y >= MeteorManager.singleton.spawnCeiling) {
			PassivateObject();
		}
	}
	
	//Activate the projectile and move it to the active set
	public void ActivateObject() {
		currentSpeed = startingSpeed;
		projectileCollider.enabled = true;

		TurretManager.singleton.ActivateProjectiles.Add(this);
		TurretManager.singleton.PassivatedProjectiles.Remove(this);
    }

	//Passivate the projectile and move it to the pooled set
	//NOTE ---> All pooled projectile stored at same location so collider 
	//			disabled to avoid inefficient OnTriggerEnter calls
    public void PassivateObject() {
		currentSpeed = 0;
		projectileCollider.enabled = false;
		transform.position = new Vector3(0, -900, 0);

		TurretManager.singleton.ActivateProjectiles.Remove(this);
		TurretManager.singleton.PassivatedProjectiles.Add(this);
    }

	//Damage the meteor if hit and create an explosion then passivate
	void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Meteor")) {
			MeteorController meteor = other.GetComponent<MeteorController>();

			//If this shot killed the meteor increase and update the score values appropriately
			if (meteor.Damage(1) == true) {	
				ScoreManager.singleton.IncreaseScore(5);
				ScoreManager.singleton.IncrementMeteorsDestroyed();
				UIManager.singleton.UpdateCurrentScores();
			}

			GameObject explosionObject = Instantiate(
				explosionPrefab, 
				transform.position, 
				Quaternion.LookRotation(transform.position - other.transform.position)
			);

			explosionObject.transform.SetParent(other.transform);
			Destroy(explosionObject, 3f);

			PassivateObject();
		}
	}   
}
