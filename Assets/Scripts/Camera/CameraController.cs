using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraViewMode {
    FRONT, TOP, LEFT
}

public enum CameraRotationMode {
    IDLE, CLOCKWISE, COUNTER_CLOCKWISE
}

public enum CameraMovementnMode {
    IDLE, FORWARDS, BACKWARDS
}

public class CameraController : MonoBehaviour {
    public float transitionTime;

    private Camera mainCam;
    public CameraViewMode viewMode;
    public CameraRotationMode rotationMode;
    public CameraMovementnMode movementMode;

    private Vector3 goalPos;
    private Vector3 startPos;
    private Quaternion startRot;
    private Quaternion goalRot;
    public byte inTransition;
    private float elapsedTime;
    private float goalConstantRotationSpeed;
    private float startConstantRotationSpeed;
    private float currentConstantRotationSpeed;


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
        if (inTransition > 0) {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionTime;
            t = t * t * t * (t * (6f * t - 15f) + 10f);

            if((inTransition & 1) > 0) {
                mainCam.transform.localPosition = Vector3.Lerp(startPos, goalPos, t);
                mainCam.transform.localRotation = Quaternion.Lerp(startRot, goalRot, t);
            }
            if((inTransition & 2) > 0) {
                currentConstantRotationSpeed = Mathf.Lerp(startConstantRotationSpeed, goalConstantRotationSpeed, t);
            }

            if (elapsedTime > transitionTime) {
                if (inTransition == 1) {
                    inTransition = 0;
                    //SetRotationMode(rotationMode);
                } else {
                    inTransition = 0;
                }

            }
        }

        transform.Rotate(Vector3.up * Time.deltaTime * currentConstantRotationSpeed);
    }

    public void SetViewMode(CameraViewMode _viewMode) {
        viewMode = _viewMode;
        switch (_viewMode) {
            case CameraViewMode.FRONT:
                goalPos = transform.position - (Vector3.forward * 10) + (transform.up * 5);
                goalRot = Quaternion.Euler(new Vector3(10, 0, 0));
                break;
            case CameraViewMode.TOP:
                goalPos = transform.position + (Vector3.up * 12);
                goalRot = Quaternion.Euler(new Vector3(90, 0, 0));
                break;
            case CameraViewMode.LEFT:
                goalPos = transform.position - (Vector3.right * 10) + transform.up * 5;
                goalRot = Quaternion.Euler(new Vector3(10, 90, 0));
                break;
        }
    }

    public void SetRotationMode(CameraRotationMode _rotMode) {
        rotationMode = _rotMode;
        startConstantRotationSpeed = currentConstantRotationSpeed;
        switch (_rotMode) {
            case CameraRotationMode.CLOCKWISE:
                goalConstantRotationSpeed += 15;
                break;
            case CameraRotationMode.COUNTER_CLOCKWISE:
                goalConstantRotationSpeed += -15;
                break;
            case CameraRotationMode.IDLE:
                goalConstantRotationSpeed = 0;
                break;
        }
        inTransition = 2;
        elapsedTime = 0;
    }

    public void SetCameraMode(CameraViewMode _viewMode, CameraRotationMode _rotationMode, CameraMovementnMode _movementMode) {
        
    }

    public void UpdateRotation() {
        switch (rotationMode) {
            case CameraRotationMode.CLOCKWISE:
                transform.Rotate(Vector3.up * Time.deltaTime * 15);
                break;
            case CameraRotationMode.COUNTER_CLOCKWISE:
                transform.Rotate(-Vector3.up * Time.deltaTime * 15);
                break;
        }
    }

    public void UpdatePositionInstant() {
        mainCam.transform.position = goalPos;
        if(inTransition > 0) {
            inTransition = 0;
        }
    }

    public void UpdatePosition() {
        startPos = mainCam.transform.localPosition;
        startRot = mainCam.transform.localRotation;
        if (Vector3.Distance(startPos, goalPos) < 0.01f) {
            return;
        }
        elapsedTime = 0;
        inTransition = 1;
    }
}
