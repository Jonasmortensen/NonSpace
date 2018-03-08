using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour{
    public CameraManager camController;
    //public Spawner spawner;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //CAMERA

        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            //Frontal
            camController.SetCameraMode(CameraMode.FRONTAL);
            camController.StartTransition();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            //Above
            camController.SetCameraMode(CameraMode.ABOVE);
            camController.StartTransition();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            //Side
            camController.SetCameraMode(CameraMode.SIDE);
            camController.StartTransition();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            //Below
            camController.SetCameraMode(CameraMode.BELOW);
            camController.StartTransition();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            //Reset rotation
        }
        /*
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
            spawner.spawn(SpawnDirection.LEFT);
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            //Center
            spawner.spawn(SpawnDirection.CENTER);
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            //Right
            spawner.spawn(SpawnDirection.RIGHT);
        }
        if (Input.GetKeyDown(KeyCode.V)) {
            //Fill
            spawner.Fill();
        }
        if (Input.GetKeyDown(KeyCode.N)) {
            //Clear
            spawner.Clear();
        }
        */
    }
}
