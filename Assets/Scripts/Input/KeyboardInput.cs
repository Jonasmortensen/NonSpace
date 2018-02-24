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
		if(Input.GetKeyDown(KeyCode.Q)) {
            camController.SetMovementMode(CameraViewMode.FRONT);
            camController.UpdatePosition(transitionTime);
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            camController.SetMovementMode(CameraViewMode.TOP);
            camController.UpdatePosition(transitionTime);
        }
    }
}
