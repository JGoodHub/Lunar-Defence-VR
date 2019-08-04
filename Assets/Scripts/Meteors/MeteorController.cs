using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO ---> Migrate projectile and meteor to superclass

public class MeteorController : MonoBehaviour, IPoolObject {   

    //VARIABLES

	//Targeting attributes
	private Vector3 targetPosition;
	private Vector3 directionToTarget;
	public Vector3 DirectionToTarget { get => directionToTarget; }

	[Header("Speed Attributes")]
	public float startingSpeed;
	private float currentSpeed;

	[Header("Health Attributes")]
	public int startingHealth;
	private int currentHealth;

	[Header("Random Rotation Attributes")]
	public float rotationSpeedMax;
	public float rotationSpeedMin;
	private float currentRotationSpeed;
	public Transform modelTransform;
	private Vector3 rotationAxis;

	[Header("Death Attributes")]
	public GameObject explosionPrefab;

    //METHODS

	//Set the target position of the meteor
	public void SetTargetPosition (Vector3 newTargetPosition) {
		targetPosition = newTargetPosition;
		directionToTarget = (targetPosition - transform.position).normalized;
	}

	//Move the meteor toward the target and spin it (looks more realistic)
	void Update () {
		transform.position += (directionToTarget * currentSpeed) * Time.deltaTime;
		modelTransform.Rotate(rotationAxis, currentRotationSpeed * Time.deltaTime);
	}

	//Set this meteor at the turrets target
	public void SetAsTarget () {
		TurretManager.instance.SetTurretsTarget(this);
	}

	//Get this meteors velocity per second
	public Vector3 MeteorVelocity () {
		return directionToTarget * currentSpeed;
	}
	
	//Damage and explode if the meteor hits a habitat
	void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Building")) {
			HabitatController habitat = other.GetComponentInParent<HabitatController>();

			//If this meteor destroyed the habitat reduce the number remaining
			if (habitat.Damage(1) == true) {
				HabitatManager.instance.DecrementHabitatsRemaining();
			}
			
			Damage(currentHealth);
		}
	}

	//Damage and or passivate the meteor with a boom
	public bool Damage (int amount) {
		currentHealth -= amount;

		if (currentHealth <= 0) {
			//If this meteor WAS the target, reset the turrets new target
			if (TurretManager.instance.TargetMeteor == this) {
				TurretManager.instance.SetTurretsTarget(null);
			}

			GameObject explosionClone = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			explosionClone.transform.SetParent(MeteorManager.instance.transform);
			Destroy(explosionClone, 10f);
			
			PassivateObject();

			return true;
		} else {
			return false;
		}
	}
	
	//Activate the pooled meteor instance
	public void ActivateObject() {
		currentSpeed = startingSpeed;
		currentHealth = startingHealth;

		//Give it a new random spin
		currentRotationSpeed = Random.Range(rotationSpeedMin, rotationSpeedMax);
		rotationAxis = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

		MeteorManager.instance.ActivateMeteors.Add(this);
		MeteorManager.instance.PassivatedMeteors.Remove(this);
    }

	//Passivate the pooled meteor instance
    public void PassivateObject() {
		currentSpeed = 0;
		currentRotationSpeed = 0;
		transform.position = new Vector3(0, -1000, 0);

		MeteorManager.instance.ActivateMeteors.Remove(this);
		MeteorManager.instance.PassivatedMeteors.Add(this);
    }

	//GIZMOS
	public bool drawDirectionGizmos;
	public bool drawRotationGizmos;
	void OnDrawGizmos () {
		if (drawDirectionGizmos) {
			Gizmos.color = Color.red;
			Gizmos.DrawRay(transform.position, directionToTarget * 10f);
		}

		if (drawRotationGizmos && Application.isPlaying) {
			Gizmos.color = Color.cyan;
			Gizmos.DrawLine(transform.position - rotationAxis * 10f, transform.position + rotationAxis * 10f);
		}
	}
}
