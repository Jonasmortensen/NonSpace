using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMode {
    FRONTAL, ABOVE, SIDE, BELOW, RESET
}

public enum CameraMovementnMode {
    IDLE, FORWARDS, BACKWARDS
}

public class CameraManager : MonoBehaviour {
    private float transitionTime = 1.5f;

    private Camera mainCam;
    public CameraMode viewMode;
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

    //For constant rotation
    private float dollyRotationSpeed;
    private float cameraRotationSpeed;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (inTransition)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionTime;
            t = t * t * t * (t * (6f * t - 15f) + 10f);

            if ((transitionType & 1) > 0)
            {
                mainCam.transform.localPosition = Vector3.Lerp(startPos, goalPos, t);
                mainCam.transform.localRotation = Quaternion.Lerp(startAngle, goalAngle, t);
            }

            if ((transitionType & 4) > 0)
            {
                transform.rotation = Quaternion.Lerp(dollyStartRot, dollyGoalRot, t);
            }

            if (elapsedTime > transitionTime)
            {
                inTransition = false;
            }
        }

        if (cameraRotationSpeed > 0.0001 || cameraRotationSpeed < -0.0001) {
            mainCam.transform.Rotate(Vector3.forward * Time.deltaTime * cameraRotationSpeed);
        }

        if(dollyRotationSpeed > 0.001 || dollyRotationSpeed < -0.0001) {
            transform.Rotate(Vector3.up * Time.deltaTime * dollyRotationSpeed);
        }
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

    //Based on 0 - 1
    public void SetCameraRotation(float value) {
        value = 2 * value - 1f;
        value *= Mathf.Abs(value);
        cameraRotationSpeed = -value * 180;
    }

    //Based on 0 - 1
    public void SetDollyRotation(float value) {
        value = 2 * value - 1f;
        value *= Mathf.Abs(value);
        dollyRotationSpeed = -value * 180;
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
        camStartRot = mainCam.transform.rotation;
        camGoalRot = Quaternion.identity;
        SetCameraMode(viewMode);
        transitionType |= 4;
        StartTransition();
        dollyRotationSpeed = 0;
        cameraRotationSpeed = 0;
        Debug.Log("Resetting!");
    }

    public void StartTransition() {
        elapsedTime = 0;
        inTransition = true;
    }
}
