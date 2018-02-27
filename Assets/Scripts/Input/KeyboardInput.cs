using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour {
    public CameraController camController;
    public Spawner spawner;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //CAMERA
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            //Frontal
            camController.SetViewMode(CameraViewMode.FRONT);
            camController.StartTransition();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            //Above
            camController.SetViewMode(CameraViewMode.TOP);
            camController.StartTransition();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            //Side
            camController.SetViewMode(CameraViewMode.LEFT);
            camController.StartTransition();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            //Below
            camController.SetViewMode(CameraViewMode.BOT);
            camController.StartTransition();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            //Reset rotation
        }

        //PLAYBACK
        if (Input.GetKeyDown(KeyCode.Q)) {
            //Real-time
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            //Shifted
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            //Back n' Forth
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            //Glitch
        }
        if (Input.GetKeyDown(KeyCode.Y)) {
            //Sync
        }

        //PLACEMENT
        if (Input.GetKeyDown(KeyCode.A)) {
            //Triangle
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            //Random
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            //Lines
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            //Ultra random
        }
        if (Input.GetKeyDown(KeyCode.H)) {
            //Sync
        }

        //SPAWN
        if (Input.GetKeyDown(KeyCode.Z)) {
            //Left
            spawner.spawn();
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            //Center
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            //Righ
        }
        if (Input.GetKeyDown(KeyCode.V)) {
            //Fill
        }
        if (Input.GetKeyDown(KeyCode.N)) {
            //Clear
        }
    }
}
