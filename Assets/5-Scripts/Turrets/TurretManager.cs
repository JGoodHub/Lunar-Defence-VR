using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour {

    //SINGLETON PATTERN

    public static TurretManager singleton = null;
	
	void Awake () {
		if (singleton == null) {
			singleton = this;
		} else {
			Destroy(gameObject);
		}
	}

    //VARIABLES

	[Header("Turret Attribute")]
	public TurretController[] turrets = new TurretController[3];
	
	[Header("Projectile Attribute")]
	public GameObject projectilePrefab;
	private float projectileSpeed;
	public float ProjectileSpeed { get => projectileSpeed; }

	//Curret target meteor
	private MeteorController targetMeteor;
	public MeteorController TargetMeteor { get => targetMeteor; }

	//Pools for activate and passivated projectiles
	//TODO ---> Move to own class, see interface
	private const int passivatedPoolSize = 50;
	private List<ProjectileController> activeProjectiles = new List<ProjectileController>();
	public List<ProjectileController> ActivateProjectiles { get => activeProjectiles; }

	private List<ProjectileController> passivatedProjectiles = new List<ProjectileController>();
	public List<ProjectileController> PassivatedProjectiles { get => passivatedProjectiles; }

    //METHODS

	//Setup the managers attributes
	public void InitialiseManager () {
		FillProjectilePool();

		projectileSpeed = projectilePrefab.GetComponent<ProjectileController>().startingSpeed;
	}

	//Fill the projectile object pool with passivated projectiles
	private void FillProjectilePool () {
		int passivatedCount = passivatedProjectiles.Count;
		while (passivatedCount < passivatedPoolSize) {
			GameObject projectileObject = Instantiate(projectilePrefab, Vector3.zero, Quaternion.identity);
			projectileObject.transform.parent = this.transform;

			ProjectileController projectileController = projectileObject.GetComponent<ProjectileController>();
			projectileController.InitialiseController();
			projectileController.PassivateObject();
			passivatedCount++;
		}
	}

	//Get a passivated projectile from the pool
	public ProjectileController GetPassivatedProjectile () {
		if (passivatedProjectiles.Count == 0) {
			FillProjectilePool();
		}

		return passivatedProjectiles[0];
	}

	//Set the target for the turrets to fire at, or disable targeting if the games over
	public void SetTurretsTarget (MeteorController newTargetMeteor) {
		if (GameManager.singleton.gameOver == true) {
			newTargetMeteor = null;
		}

		targetMeteor = newTargetMeteor;
		foreach (TurretController turretControl in turrets) {
			turretControl.TargetMeteor = newTargetMeteor;
		}
	} 
    
}
