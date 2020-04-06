using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {

    //VARIABLES

	//Turrets current target meteor
	//NOTE ---> Separate from the manager to allow for individual targeting as a future feature
	private MeteorController targetMeteor;
	public MeteorController TargetMeteor { set => targetMeteor = value; }

	[Header("Turret Firing Attributes")]
	public Transform turretTransform;
	public float rateOfFire;
	private float fireCooldown;

    //METHODS
	
	//Track the current target meteor and fire on it, else reset the rotation to a standby mode
	void Update () {
		fireCooldown -= Time.deltaTime;
		if (targetMeteor != null) {
			Vector3 leadingTargetPosition = CalculateLeadingTarget(turretTransform.position,
																   targetMeteor.transform.position,
																   targetMeteor.MeteorVelocity(),
																   TurretManager.singleton.ProjectileSpeed);
			turretTransform.LookAt(leadingTargetPosition);

			//Fire with a slight time variance to avoid repeating patterns
			if (fireCooldown <= 0) {
				FireProjectile();
				fireCooldown = rateOfFire + Random.Range(-0.10f, 0.10f);
			}
		} else {
			turretTransform.LookAt(transform.position + transform.up);
		}
	}

	//Calculate the leading position of a moving target using its velocity and that of the projectile
	//NOTE ---> Formula taken from: http://www.tosos.com/pages/calculating-a-lead-on-a-target
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

	//Fire a projectile in the current turret direction
	private void FireProjectile () {
		ProjectileController projectileInstance = TurretManager.singleton.GetPassivatedProjectile();
		projectileInstance.ActivateObject();
		
		projectileInstance.transform.position = turretTransform.position;
		projectileInstance.transform.forward = turretTransform.forward;
		
		//Offset the projectile so its outside the barrel
		projectileInstance.transform.position += projectileInstance.transform.forward * 3f;
	}
    
}
