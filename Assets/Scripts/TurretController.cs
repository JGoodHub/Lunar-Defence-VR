using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {

    //VARIABLES

	private Transform trackingtarget;
	public Transform TrackingTarget { set => trackingtarget = value; }

	public Transform turretTransform;

    //METHODS
	
	void Update () {
		turretTransform.LookAt(trackingtarget);
	}
    
    
}
