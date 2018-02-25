using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour {
    public CameraController camController;
    public float transitionTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Keypad7)) {
            camController.SetViewMode(CameraViewMode.FRONT);
            camController.UpdatePosition(transitionTime);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8)) {
            camController.SetViewMode(CameraViewMode.TOP);
            camController.UpdatePosition(transitionTime);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9)) {
            camController.SetViewMode(CameraViewMode.LEFT);
            camController.UpdatePosition(transitionTime);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4)) {
            camController.SetRotationMode(CameraRotationMode.IDLE);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5)) {
            camController.SetRotationMode(CameraRotationMode.CLOCKWISE);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6)) {
            camController.SetRotationMode(CameraRotationMode.COUNTER_CLOCKWISE);
        }
    }
}
