using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraViewMode {
    FRONT, TOP, LEFT
}

public enum CameraRotationMode {
    IDLE, SLOW, FAST
}

public enum CameraMovementnMode {
    IDLE, FORWARDS, BACKWARDS
}

public class CameraController : MonoBehaviour {

    private Camera mainCam;
    private CameraViewMode viewMode;

    private Vector3 goalPos;
    private Vector3 startPos;
    private bool inTransition;
    private float elapsedTime;
    private float transitionTime;


	// Use this for initialization
	void Start () {
        mainCam = GetComponentInChildren<Camera>();
        if(mainCam == null) {
            throw new System.NullReferenceException("CameraController needs a child camera");
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (inTransition) {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionTime;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            mainCam.transform.position = Vector3.Lerp(startPos, goalPos, t);
            if (elapsedTime > transitionTime) {
                inTransition = false;
            }
        }

        mainCam.transform.LookAt(transform.position);
    }

    public void SetMovementMode(CameraViewMode movementMode) {
        switch (movementMode) {
            case CameraViewMode.FRONT:
                goalPos = transform.position - (transform.forward * 10) + (transform.up * 5);
                break;
            case CameraViewMode.TOP:
                goalPos = transform.position + (transform.up * 10);
                break;
        }
    }

    public void SetMovementMode(CameraViewMode viewMode, CameraRotationMode rotationMode, CameraMovementnMode movementMode) {
        switch (viewMode) {
            case CameraViewMode.FRONT:
                goalPos = transform.position - (transform.forward * 10) + (transform.up * 5);
                break;
            case CameraViewMode.TOP:
                goalPos = transform.position + (transform.up * 10);
                break;
            case CameraViewMode.LEFT:
                goalPos = transform.position - (transform.right * 10 + transform.up * 5);
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
        elapsedTime = 0;
        transitionTime = time;
        startPos = mainCam.transform.position;
        inTransition = true;
    }
}
