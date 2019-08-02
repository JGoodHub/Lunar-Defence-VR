using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour {

    //SINGLETON PATTERN

    public static TurretManager instance = null;
	
	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
		}
	}

    //VARIABLES

	public TurretController[] turrets = new TurretController[3];
	[HideInInspector] public MeteorController targetMeteor;

	public GameObject projectilePrefab;

	private const int passivatedPoolSize = 50;
	private List<ProjectileController> activeProjectiles = new List<ProjectileController>();
	public List<ProjectileController> ActivateProjectiles { get => activeProjectiles; }

	private List<ProjectileController> passivatedProjectiles = new List<ProjectileController>();
	public List<ProjectileController> PassivatedProjectiles { get => passivatedProjectiles; }

    //METHODS

	public void InitialiseManager () {
		FillProjectilePool();
	}

	private void FillProjectilePool () {
		int passivatedCount = passivatedProjectiles.Count;
		while (passivatedCount < passivatedPoolSize) {
			GameObject projectileObject = Instantiate(projectilePrefab, Vector3.zero, Quaternion.identity);
			projectileObject.transform.parent = this.transform;

			ProjectileController projectileController = projectileObject.GetComponent<ProjectileController>();
			projectileController.Initialise();
			projectileController.PassivateObject();
			passivatedCount++;
		}
	}

	public ProjectileController GetPassivatedProjectile () {
		if (passivatedProjectiles.Count == 0) {
			FillProjectilePool();
		}

		return passivatedProjectiles[0];
	}

	public void SetTargetMeteor (MeteorController newTargetMeteor) {
		targetMeteor = newTargetMeteor;
		foreach (TurretController turretControl in turrets) {
			turretControl.TargetMeteorTransform = newTargetMeteor.transform;
		}
	}

	//GIZMOS	
	void OnDrawGizmos () {
		
	}    
    
}
