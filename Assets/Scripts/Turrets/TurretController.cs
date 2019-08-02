using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {

    //VARIABLES

	private Transform targetMeteorTransform;
	public Transform TargetMeteorTransform { set => targetMeteorTransform = value; }

	public Transform turretTransform;
	public float rateOfFire;
	private float fireCooldown;

    //METHODS
	
	void Update () {
		fireCooldown -= Time.deltaTime;
		if (targetMeteorTransform != null) {
			AimTurretAtLeadingTarget();
			
			if (fireCooldown <= 0) {
				FireProjectile();
				fireCooldown = rateOfFire + Random.Range(-0.15f, 0.15f);
			}
		}
	}

	private void AimTurretAtLeadingTarget () {
		//TODO ---> Aim the turret ahead of the meteor so that the projectile always hits
		turretTransform.LookAt(targetMeteorTransform);
		Debug.DrawRay(turretTransform.position, targetMeteorTransform.position - turretTransform.position);
	}

	private void FireProjectile () {
		ProjectileController projectileInstance = TurretManager.instance.GetPassivatedProjectile();
		projectileInstance.ActivateObject();
		
		projectileInstance.transform.position = turretTransform.position;
		projectileInstance.transform.forward = turretTransform.forward;
		
		projectileInstance.transform.position += projectileInstance.transform.forward * 3f;

	}
    
}
