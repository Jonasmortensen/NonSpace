using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour{
    public CameraManager camManager;
    //public Spawner spawner;

    private float camRotVal;
    private float dollyRotVal;

    // Use this for initialization
    void Start () {
        camRotVal = 0.5f;
        dollyRotVal = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
        //CAMERA

        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            //Frontal
            camManager.SetCameraMode(CameraMode.FRONTAL);
            camManager.StartTransition();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            //Above
            camManager.SetCameraMode(CameraMode.ABOVE);
            camManager.StartTransition();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            //Side
            camManager.SetCameraMode(CameraMode.SIDE);
            camManager.StartTransition();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            //Below
            camManager.SetCameraMode(CameraMode.BELOW);
            camManager.StartTransition();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            camRotVal = 0.5f;
            dollyRotVal = 0.5f;
            camManager.SetDollyRotation(dollyRotVal);
            camManager.SetCameraRotation(camRotVal);
            camManager.ResetRotation();
        }

        if(Input.GetKeyDown(KeyCode.O)) {
            camRotVal += 0.1f;
            Mathf.Clamp(camRotVal, 0.0f, 1.0f);
            camManager.SetCameraRotation(camRotVal);
        }

        if(Input.GetKeyDown(KeyCode.P)) {
            camRotVal += -0.1f;
            Mathf.Clamp(camRotVal, 0.0f, 1.0f);
            camManager.SetCameraRotation(camRotVal);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            dollyRotVal += 0.1f;
            Mathf.Clamp(dollyRotVal, 0.0f, 1.0f);
            camManager.SetDollyRotation(dollyRotVal);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            dollyRotVal += -0.1f;
            Mathf.Clamp(dollyRotVal, 0.0f, 1.0f);
            camManager.SetDollyRotation(dollyRotVal);
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
