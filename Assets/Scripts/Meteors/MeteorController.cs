using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO ---> Migrate projectile and meteor to superclass

public class MeteorController : MonoBehaviour, IPoolObject {   

    //VARIABLES

	private Vector3 targetPosition;
	private Vector3 directionToTarget;
	public Vector3 DirectionToTarget { get => directionToTarget; }

	public float startingSpeed;
	private float currentSpeed;

	public int startingHealth;
	private int currentHealth;

	public float rotationSpeedMax;
	public float rotationSpeedMin;
	public Transform modelTransform;
	private float currentRotationSpeed;
	private Vector3 rotationAxis;

	public GameObject explosionPrefab;

    //METHODS

	public void SetTargetPosition (Vector3 newTargetPosition) {
		targetPosition = newTargetPosition;
		directionToTarget = (targetPosition - transform.position).normalized;
	}

	void Update () {
		transform.position += (directionToTarget * currentSpeed) * Time.deltaTime;
		modelTransform.Rotate(rotationAxis, currentRotationSpeed * Time.deltaTime);
	}

	public void SetAsTarget () {
		TurretManager.instance.SetTurretsTarget(this);
	}

	public Vector3 MeteorVelocity () {
		return directionToTarget * currentSpeed;
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Building")) {
			HabitatController habitat = other.GetComponentInParent<HabitatController>();
			habitat.Damage(1);
			
			Damage(currentHealth);
		}
	}

	public void Damage (int amount) {
		currentHealth -= amount;

		if (currentHealth <= 0) {
			GameObject explosionClone = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			explosionClone.transform.SetParent(MeteorManager.instance.transform);
			Destroy(explosionClone, 10f);
			
			//TODO ---> Add to the players score

			PassivateObject();
		}
	}

	//INTERFACES
	
	public void ActivateObject() {
		currentSpeed = startingSpeed;
		currentHealth = startingHealth;

		currentRotationSpeed = Random.Range(rotationSpeedMin, rotationSpeedMax);
		rotationAxis = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

		MeteorManager.instance.ActivateMeteors.Add(this);
		MeteorManager.instance.PassivatedMeteors.Remove(this);
    }

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
