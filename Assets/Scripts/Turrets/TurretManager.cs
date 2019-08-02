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

	[Header("Dummy Attributes")]
	public Vector3 dummyStartingPosition;
	public float dummySpeed;

	public Transform dummyTransform;
	private Vector3 dummyDirection;

	public float dummyRefreshRate;
	private float dummyRefreshCountdown = 0;

    //METHODS

	public void InitialiseManager() {
		foreach (TurretController turretControl in turrets) {
			turretControl.TrackingTarget = dummyTransform;
		}
	}

	void Update () {
		dummyRefreshCountdown -= Time.deltaTime;
		if (dummyRefreshCountdown <= 0) {
			dummyTransform.position = dummyStartingPosition;
			
			dummyDirection = new Vector3(1f - Random.Range(0f, 2f), 1f - Random.Range(0f, 2f), 1f - Random.Range(0f, 2f));
			dummyDirection.Normalize();

			dummyRefreshCountdown = dummyRefreshRate;
		} else {
			dummyTransform.position += (dummyDirection * dummySpeed) * Time.deltaTime;
		}

	}

	//GIZMOS
	public bool drawDummyGizmos;
	
	void OnDrawGizmos () {
		if (drawDummyGizmos) {
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(dummyStartingPosition, 1f);

			if (Application.isPlaying) {
				Gizmos.color = Color.magenta;
				Gizmos.DrawSphere(dummyTransform.position, 0.5f);
			}
		}
	}    
    
}
