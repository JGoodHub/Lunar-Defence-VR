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

	public float startingHealth;
	private float currentHealth;

    //METHODS

	public void SetTargetPosition (Vector3 newTargetPosition) {
		targetPosition = newTargetPosition;
		directionToTarget = (targetPosition - transform.position).normalized;
	}

	void Update () {
		transform.position += (directionToTarget * currentSpeed) * Time.deltaTime;
	}

	public void SetAsTarget () {
		TurretManager.instance.SetTurretsTarget(this);
		Debug.Log("Target Acquired");
	}

	public Vector3 MeteorVelocity () {
		return directionToTarget * currentSpeed;
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Building")) {
			PassivateObject();
		}
	}

	//INTERFACES
	
	public void ActivateObject() {
		currentSpeed = startingSpeed;

		MeteorManager.instance.ActivateMeteors.Add(this);
		MeteorManager.instance.PassivatedMeteors.Remove(this);
    }

    public void PassivateObject() {
		currentSpeed = 0;
		transform.position = new Vector3(0, -1000, 0);

		MeteorManager.instance.ActivateMeteors.Remove(this);
		MeteorManager.instance.PassivatedMeteors.Add(this);
		
		TurretManager.instance.SetTurretsTarget(null);
    }


	//GIZMOS
	public bool drawDirectionGizmos;

	void OnDrawGizmos () {
		if (drawDirectionGizmos) {
			Gizmos.color = Color.red;
			Gizmos.DrawRay(transform.position, directionToTarget * 10f);
		}
	}


}
