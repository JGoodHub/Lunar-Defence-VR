using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {

    //VARIABLES

	private MeteorController targetMeteor;
	public MeteorController TargetMeteor { set => targetMeteor = value; }

	public Transform turretTransform;
	public float rateOfFire;
	private float fireCooldown;

    //METHODS
	
	void Update () {
		fireCooldown -= Time.deltaTime;
		if (targetMeteor != null) {
			Vector3 leadingTargetPosition = CalculateLeadingTarget(turretTransform.position,
																   targetMeteor.transform.position,
																   targetMeteor.MeteorVelocity(),
																   TurretManager.instance.ProjectileSpeed);
			turretTransform.LookAt(leadingTargetPosition);

			if (fireCooldown <= 0) {
				FireProjectile();
				fireCooldown = rateOfFire + Random.Range(-0.15f, 0.15f);
			}
		} else {
			turretTransform.LookAt(transform.position + transform.up);
		}
	}

	public Vector3 CalculateLeadingTarget (Vector3 shooterPosition, Vector3 targetPosition, Vector3 targetVelocity, float projectileSpeed) {
		float A = targetVelocity.sqrMagnitude - Mathf.Pow(projectileSpeed, 2);
		float B = Vector3.Dot(2 * (targetPosition - shooterPosition), targetVelocity);
		float C = (targetPosition - shooterPosition).sqrMagnitude;

		if (A >= 0) {
			Debug.LogError ("No solution exists");
			return targetPosition;
		} else {
			float rightTerm = Mathf.Sqrt((B * B) - (4 * A *C));
			float dt1 = (-B + rightTerm) / (2 * A);
			float dt2 = (-B - rightTerm) / (2 * A);
			float travelTime = (dt1 < 0 ? dt2 : dt1);
			return targetPosition + (targetVelocity * travelTime);
		}
	}

	private void FireProjectile () {
		ProjectileController projectileInstance = TurretManager.instance.GetPassivatedProjectile();
		projectileInstance.ActivateObject();
		
		projectileInstance.transform.position = turretTransform.position;
		projectileInstance.transform.forward = turretTransform.forward;
		
		projectileInstance.transform.position += projectileInstance.transform.forward * 3f;
	}
    
}
