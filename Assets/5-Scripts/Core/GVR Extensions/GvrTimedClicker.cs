using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GvrTimedClicker : MonoBehaviour {

    //Structs

    public struct PointerEventArgs {
        public Transform target;
        public Vector3 point;
        public Vector3 normal;

        public PointerEventArgs(Transform _target, Vector3 _point, Vector3 _normal) {
            target = _target;
            point = _point;
            normal = _normal;
        }
    }

    //Events

    public delegate void OnPointerEnterHandler(object sender, PointerEventArgs e);
    public static event OnPointerEnterHandler OnPointerEnter;

    public delegate void OnPointerExitHandler(object sender, PointerEventArgs e);
    public static event OnPointerExitHandler OnPointerExit;

    public delegate void OnPointerClickedHandler(object sender, PointerEventArgs e);
    public static event OnPointerClickedHandler OnPointerClicked;

    public delegate void OnPointerDownHandler(object sender, PointerEventArgs e);
    public static event OnPointerDownHandler OnPointerDown;

    [Header("Timing Parameters")]
    public float timeToClick;

    //State
    private PointerEventArgs prevPointerArgs;
    private PointerEventArgs currPointerArgs;
    private RaycastHit pointerHit;

    private bool enteredTriggered;
    private float timeToClickCountdown;
    private bool clickTriggered;

    #region Inherited Methods

    private void Start() {
        timeToClickCountdown = timeToClick;
        clickTriggered = false;

        OnPointerEnter += TargetEntered;
        OnPointerExit += TargetExited;
        OnPointerClicked += TargetClicked;
        OnPointerDown += TargetDown;
    }

    private void Update() {
        //Raycast from the current camera forward
        if (Physics.Raycast(transform.position, transform.forward, out pointerHit, 250f)) {
            //Update the current pointer args
            currPointerArgs.target = pointerHit.transform;
            currPointerArgs.point = pointerHit.point;
            currPointerArgs.normal = pointerHit.normal;

            if (prevPointerArgs.target == null || pointerHit.transform == prevPointerArgs.target) {
                timeToClickCountdown -= Time.deltaTime;

                //Send the entered event
                if (enteredTriggered == false) {
                    OnPointerEnter?.Invoke(this, currPointerArgs);
                    enteredTriggered = true;
                }

                if (timeToClickCountdown <= 0) {
                    //Send the one time click event
                    if (clickTriggered == false) {
                        OnPointerClicked?.Invoke(this, currPointerArgs);
                        ExecuteEvents.Execute(pointerHit.transform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                        clickTriggered = true;
                    }

                    OnPointerDown?.Invoke(this, currPointerArgs);

                    //Set the previous hit to the new target
                    prevPointerArgs.target = pointerHit.transform;
                    prevPointerArgs.point = pointerHit.point;
                    prevPointerArgs.normal = pointerHit.normal;
                }
            } else {
                //Exit the old target and enter the new one
                OnPointerExit?.Invoke(this, prevPointerArgs);

                OnPointerEnter?.Invoke(this, currPointerArgs);

                //Set the previous hit to the new target
                prevPointerArgs.target = pointerHit.transform;
                prevPointerArgs.point = pointerHit.point;
                prevPointerArgs.normal = pointerHit.normal;

                //Reset state variables
                timeToClickCountdown = timeToClick;
                enteredTriggered = false;
                clickTriggered = false;
            }
        } else {
            if (prevPointerArgs.target != null) {
                //Send the exit event
                OnPointerExit?.Invoke(this, prevPointerArgs);

                //Reset state variables
                prevPointerArgs.target = null;
                timeToClickCountdown = timeToClick;
                enteredTriggered = false;
                clickTriggered = false;
            } else {
                timeToClickCountdown = timeToClick;
            }
        }

        //Draw the debug gizmos
        if (drawDebug) {
            Debug.DrawLine(transform.position, prevPointerArgs.point, Color.red);
        }
    }

    #endregion

    #region Public Methods

    #endregion

    #region Private Methods

    private void TargetEntered(object sender, PointerEventArgs e) {
        Debug.Log(e.target.name + " Entered");
    }

    private void TargetExited(object sender, PointerEventArgs e) {
        Debug.Log(e.target.name + " Exited");
    }

    private void TargetClicked(object sender, PointerEventArgs e) {
        Debug.Log(e.target.name + " Clicked");
    }

    private void TargetDown(object sender, PointerEventArgs e) {
        Debug.Log(e.target.name + " Down");
    }

    #endregion

    #region Coroutines

    #endregion

    #region Event Handlers

    #endregion

    #region Gizmos

    public bool drawDebug;
    private void OnDrawGizmos() {

    }

    #endregion

}