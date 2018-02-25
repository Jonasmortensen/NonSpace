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

    private Camera mainCam;
    public CameraViewMode viewMode;
    public CameraRotationMode rotationMode;
    public CameraMovementnMode movementMode;

    private Vector3 goalPos;
    private Vector3 startPos;
    private Quaternion startRot;
    private Quaternion goalRot;
    public bool inTransition;
    private float elapsedTime;
    private float transitionTime;


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
            mainCam.transform.position = Vector3.Lerp(startPos, goalPos, t);
            mainCam.transform.rotation = Quaternion.Lerp(startRot, goalRot, t);

            if (elapsedTime > transitionTime) {
                inTransition = false;
            }
        }
        UpdateRotation();
        //mainCam.transform.LookAt(transform.position);
    }

    public void SetViewMode(CameraViewMode _viewMode) {
        viewMode = _viewMode;
        switch (_viewMode) {
            case CameraViewMode.FRONT:
                goalPos = transform.position - (Vector3.forward * 10) + (transform.up * 5);
                goalRot = Quaternion.Euler(new Vector3(10, 0, 0));
                break;
            case CameraViewMode.TOP:
                goalPos = transform.position + (Vector3.up * 10);
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
    }

    public void SetCameraMode(CameraViewMode _viewMode, CameraRotationMode _rotationMode, CameraMovementnMode _movementMode) {
        
    }

    public void UpdateRotation() {
        switch (rotationMode) {
            case CameraRotationMode.CLOCKWISE:
                transform.Rotate(Vector3.up * Time.deltaTime * 10);
                break;
            case CameraRotationMode.COUNTER_CLOCKWISE:
                transform.Rotate(-Vector3.up * Time.deltaTime * 10);
                break;
        }
    }

    public void UpdatePosition() {
        mainCam.transform.position = goalPos;
        if(inTransition) {
            inTransition = false;
        }
    }

    public void UpdatePosition(float time) {
        startPos = mainCam.transform.position;
        startRot = mainCam.transform.rotation;
        if (Vector3.Distance(startPos, goalPos) < 0.01f) {
            return;
        }
        elapsedTime = 0;
        transitionTime = time;
        inTransition = true;
    }
}
