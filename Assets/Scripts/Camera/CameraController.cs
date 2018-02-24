using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMovementMode {
    FRONT, TOP, ORBIT, FORWARD
}

public class CameraController : MonoBehaviour {

    private Camera mainCam;
    private CameraMovementMode movementMode;
    private Vector3 goalPos;


	// Use this for initialization
	void Start () {
        mainCam = GetComponentInChildren<Camera>();
        if(mainCam == null) {
            throw new System.NullReferenceException("CameraController needs a child camera");
        }
    }
	
	// Update is called once per frame
	void Update () {
        mainCam.transform.LookAt(transform.position);
	}

    public void SetMovementMode(CameraMovementMode movementMode) {
        switch (movementMode) {
            case CameraMovementMode.FRONT:
                goalPos = transform.position - (transform.forward * 2);
                break;
            case CameraMovementMode.TOP:
                goalPos = transform.position + (transform.up * 2);
                break;
        }
    }

    public void InstantPositionUpdate() {
        transform.position = goalPos;
    }
}
