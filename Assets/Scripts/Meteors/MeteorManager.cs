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
	public float spawnInnerRadius;
	public float spawnOuterRadius;
	public float spawnInterval;
	public float spawnDelay;

	public GameObject meteorPrefab;

	private const int passivatedPoolSize = 30;
	private List<MeteorController> activeMeteors = new List<MeteorController>();
	public List<MeteorController> ActivateMeteors { get => activeMeteors; }

	private List<MeteorController> passivatedMeteors = new List<MeteorController>();
	public List<MeteorController> PassivatedMeteors { get => passivatedMeteors; }

    //METHODS

	public void InitialiseManager () {
		FillMeteorPool();
		InvokeRepeating("HurlMeteor", spawnDelay, spawnInterval);
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

		Vector3 startingPositionOffset = (new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized);
		startingPositionOffset = (startingPositionOffset * spawnInnerRadius) + 
								 (startingPositionOffset * Random.Range(0f, spawnOuterRadius - spawnInnerRadius));
		
		activatedMeteor.transform.position = (Vector3.up * spawnCeiling) + startingPositionOffset;
		activatedMeteor.SetTargetPosition(HabitatManager.instance.GetRandomBuilding().transform.position);

		if (drawSpawnGizmos) {
			Debug.DrawRay(Vector3.up * spawnCeiling, startingPositionOffset, Color.magenta, 1f);
		}
	}

	//GIZMOS
	public bool drawSpawnGizmos;

	void OnDrawGizmos () {
		if (drawSpawnGizmos) {
			Gizmos.color = Color.green;
			
			DrawGizmosCircle(spawnInnerRadius, 32, Vector3.up * spawnCeiling);
			DrawGizmosCircle(spawnOuterRadius, 32, Vector3.up * spawnCeiling);
		}
	}

	private void DrawGizmosCircle (float radius, int segments, Vector3 center) {
		float angleStep = 360f / segments;
		float angleStepAsRad = angleStep * Mathf.Deg2Rad;

		for (float currentAngle = 0; currentAngle < 360; currentAngle += angleStep) {
			float currentAngleAsRad = currentAngle * Mathf.Deg2Rad;

			Gizmos.DrawLine(
				new Vector3 (
					(radius * Mathf.Sin(currentAngleAsRad)) + center.x,
					(center.y),
					(radius * Mathf.Cos(currentAngleAsRad)) + center.z
				),
				new Vector3 (
					(radius * Mathf.Sin(currentAngleAsRad + angleStepAsRad)) + center.x,
					(center.y),
					(radius * Mathf.Cos(currentAngleAsRad + angleStepAsRad)) + center.z
				)
			);
		}

	}
    
    
}
