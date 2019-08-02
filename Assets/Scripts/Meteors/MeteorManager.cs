using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorManager : MonoBehaviour {

    //SINGLETON PATTERN

    public static MeteorManager instance = null;
	
	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
		}
	}

    //VARIABLES

	public float spawnCeiling;
	public float spawnRadius;
	public float spawnInterval;

	public GameObject meteorPrefab;

	private const int passivatedPoolSize = 10;
	private List<MeteorController> activeMeteors = new List<MeteorController>();
	public List<MeteorController> ActivateMeteors { get => activeMeteors; }

	private List<MeteorController> passivatedMeteors = new List<MeteorController>();
	public List<MeteorController> PassivatedMeteors { get => passivatedMeteors; }

    //METHODS

	public void InitialiseManager () {
		FillMeteorPool();
		InvokeRepeating("HurlMeteor", 0, spawnInterval);
	}

	private void FillMeteorPool () {
		int passivatedCount = passivatedMeteors.Count;
		while (passivatedCount < passivatedPoolSize) {
			GameObject meteorObject = Instantiate(meteorPrefab, Vector3.zero, Quaternion.identity);
			meteorObject.transform.parent = this.transform;

			MeteorController meteorController = meteorObject.GetComponent<MeteorController>();
			meteorController.PassivateObject();
			passivatedCount++;
		}
	}

	private MeteorController GetPassivatedMeteor () {
		if (passivatedMeteors.Count == 0) {
			FillMeteorPool();
		}

		return passivatedMeteors[0];
	}

	private void HurlMeteor () {
		MeteorController activatedMeteor = GetPassivatedMeteor();
		activatedMeteor.ActivateObject();

		Vector3 startingPosition = new Vector3(Random.Range(-spawnRadius, spawnRadius), spawnCeiling, Random.Range(-spawnRadius, spawnRadius));
		activatedMeteor.transform.position = startingPosition;
		activatedMeteor.SetTargetPosition(BuildingManager.instance.GetRandomBuilding().transform.position);
	}

	//GIZMOS
	public bool drawSpawnGizmos;

	void OnDrawGizmos () {
		if (drawSpawnGizmos) {
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(new Vector3(0, spawnCeiling, 0), new Vector3(spawnRadius * 2, 0.01f, spawnRadius * 2));
			Gizmos.DrawLine(new Vector3(spawnRadius, spawnCeiling, spawnRadius), new Vector3(-spawnRadius, spawnCeiling, -spawnRadius));
			Gizmos.DrawLine(new Vector3(spawnRadius, spawnCeiling, -spawnRadius), new Vector3(-spawnRadius, spawnCeiling, spawnRadius));
		}
	}
    
    
}
