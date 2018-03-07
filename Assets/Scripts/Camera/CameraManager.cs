using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMode {
    FRONTAL, ABOVE, SIDE, BELOW, RESET
}

public enum CameraRotationMode {
    IDLE, ROTATE_DOLLY, ROTATE_CAM
}

public enum CameraMovementnMode {
    IDLE, FORWARDS, BACKWARDS
}

public class CameraManager : MonoBehaviour {
    public float transitionTime;

    private Camera mainCam;
    public CameraMode viewMode;
    public CameraRotationMode rotationMode;
    public CameraMovementnMode movementMode;

    //General transition vars
    private byte transitionType;
    private bool inTransition;
    private float elapsedTime;

    //For view point
    private Vector3 goalPos;
    private Vector3 startPos;
    private Quaternion startAngle;
    private Quaternion goalAngle;

    //For turntable rotation
    private float goalDollyRotation;
    private float startDollyRotation;
    private float currentDollyRotation;

    //For camera z rotation
    private float goalCamRotation;
    private float startCamRotation;
    private float currentCamRotation;

    //For resetting rotation
    private Quaternion dollyStartRot;
    private Quaternion dollyGoalRot;
    private Quaternion camStartRot;
    private Quaternion camGoalRot;


    // Use this for initialization
    void Start () {
        mainCam = GetComponentInChildren<Camera>();
        if(mainCam == null) {
            throw new System.NullReferenceException("CameraController needs a child camera");
        }
        rotationMode = CameraRotationMode.IDLE;
    }
	
	// Update is called once per frame
	void Update () {
        if (inTransition) {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionTime;
            t = t * t * t * (t * (6f * t - 15f) + 10f);

            if((transitionType & 1) > 0) {
                mainCam.transform.localPosition = Vector3.Lerp(startPos, goalPos, t);
                mainCam.transform.localRotation = Quaternion.Lerp(startAngle, goalAngle, t);
            }
            if((transitionType & 2) > 0) {
                currentDollyRotation = Mathf.Lerp(startDollyRotation, goalDollyRotation, t);
                currentCamRotation = Mathf.Lerp(startCamRotation, goalCamRotation, t);
            }
            if((transitionType & 4) > 0) {
                transform.rotation = Quaternion.Lerp(dollyStartRot, dollyGoalRot, t);
            }

            if (elapsedTime > transitionTime) {
                inTransition = false;
            }
        }

        transform.Rotate(Vector3.up * Time.deltaTime * currentDollyRotation);
        mainCam.transform.Rotate(Vector3.forward * Time.deltaTime * currentCamRotation);
    }

    public void SetCameraMode(CameraMode _viewMode) {
        viewMode = _viewMode;
        startPos = mainCam.transform.localPosition;
        startAngle = mainCam.transform.localRotation;
        switch (_viewMode) {
            case CameraMode.FRONTAL:
                goalPos = transform.position - (Vector3.forward * 10) + (transform.up * 5);
                goalAngle = Quaternion.Euler(new Vector3(10, 0, 0));
                break;
            case CameraMode.ABOVE:
                goalPos = transform.position + (Vector3.up * 12);
                goalAngle = Quaternion.Euler(new Vector3(90, 0, 0));
                break;
            case CameraMode.SIDE:
                goalPos = transform.position - (Vector3.right * 10) + transform.up * 5;
                goalAngle = Quaternion.Euler(new Vector3(10, 90, 0));
                break;
            case CameraMode.BELOW:
                goalPos = transform.position + (-Vector3.up * 12);
                goalAngle = Quaternion.Euler(new Vector3(-90, 0, 0));
                break;
        }
        if (Vector3.Distance(startPos, goalPos) < 0.01f && Quaternion.Angle(startAngle, goalAngle) < 1) {
            return;
        }
        transitionType = 1;
    }

    public void SetRotationMode(CameraRotationMode _rotMode) {
        rotationMode = _rotMode;
        startDollyRotation = currentDollyRotation;
        startCamRotation = currentCamRotation;
        transitionType = 2;
        switch (_rotMode) {
            case CameraRotationMode.ROTATE_DOLLY:
                goalDollyRotation -= 15;
                break;
            case CameraRotationMode.ROTATE_CAM:
                goalCamRotation += 15;
                break;
            case CameraRotationMode.IDLE:
                if (goalDollyRotation == 0 && goalCamRotation == 0) {
                    ResetRotation();
                    currentCamRotation = 0;
                    currentDollyRotation = 0;
                }
                else {
                    goalDollyRotation = 0;
                    goalCamRotation = 0;
                }
                break;
        }
    }

    public void UpdatePositionInstant() {
        mainCam.transform.position = goalPos;
        if(transitionType > 0) {
            transitionType = 0;
        }
    }

    public void ResetRotation() {
        dollyStartRot = transform.rotation;
        dollyGoalRot = Quaternion.identity;
        //camStartRot = mainCam.transform.rotation;
        //camGoalRot = Quaternion.identity;
        SetCameraMode(viewMode);
        transitionType |= 4;
        Debug.Log("Resetting!");
    }

    public void StartTransition() {
        elapsedTime = 0;
        inTransition = true;
    }
}
