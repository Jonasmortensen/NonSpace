using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMode {
    FRONTAL, ABOVE, SIDE, BELOW, RESET
}

public enum PlaybackMode {
    REAL_TIME, SHIFTED, BACKNFORTH, GLITCH, RESET
}

public enum PlacementMode {
    TRIANGLE, RANDOM, LINES, ULTRARANDOM, RESET
}

public class APC40Controller : MonoBehaviour {
    public float RotationKnob1;
    public float RotationKnob2;
    private int MovementKnob;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return)) {
            InitController();
        }

        ListenCameraInput();

    }

    public void InitController() {
        Debug.Log("Initialising!");
        TurnOffLights();
        InitiateKnobs();
        SetCameraLights(CameraMode.FRONTAL);;
        //SetPlaybackLights(PlaybackMode.REAL_TIME);
        //SetPlacementLights(PlacementMode.TRIANGLE);

        //InitSpawnLights();
    }

    private void InitiateKnobs() {
        MidiOut.SendControlChange(MidiChannel.Ch1, 0x38, 3);
        MidiOut.SendControlChange(MidiChannel.Ch1, 0x39, 3);
        MidiOut.SendControlChange(MidiChannel.Ch1, 0x3A, 1);

        MidiOut.SendControlChange(MidiChannel.Ch1, 0x3B, 0);
        MidiOut.SendControlChange(MidiChannel.Ch1, 0x3C, 0);
        MidiOut.SendControlChange(MidiChannel.Ch1, 0x3D, 0);
        MidiOut.SendControlChange(MidiChannel.Ch1, 0x3E, 0);
        MidiOut.SendControlChange(MidiChannel.Ch1, 0x3F, 0);

        ResetKnobs();
    }

    public void TurnOffLights() {
        for (int i = 0; i < 39; i++) {
            MidiOut.SendNoteOff(MidiChannel.Ch1, i);
        }

        MidiOut.SendNoteOff(MidiChannel.Ch1, 51);
    }

    private void ListenCameraInput() {
        if (MidiInput.GetKeyDown(MidiChannel.Ch1, 32)) {
            SetCameraLights(CameraMode.FRONTAL);
        }
        if (MidiInput.GetKeyDown(MidiChannel.Ch1, 33)) {
            SetCameraLights(CameraMode.ABOVE);
        }
        if (MidiInput.GetKeyDown(MidiChannel.Ch1, 34)) {
            SetCameraLights(CameraMode.SIDE);
        }
        if (MidiInput.GetKeyDown(MidiChannel.Ch1, 35)) {
            SetCameraLights(CameraMode.BELOW);
        }
        if (MidiInput.GetKeyDown(MidiChannel.Ch1, 37)) {
            SetCameraLights(CameraMode.RESET);
        }
    }

    public void SetCameraLights(CameraMode mode) {
        MidiOut.SendNoteOn(MidiChannel.Ch1, 32, 23f);
        MidiOut.SendNoteOn(MidiChannel.Ch1, 33, 23f);
        MidiOut.SendNoteOn(MidiChannel.Ch1, 34, 23f);
        MidiOut.SendNoteOn(MidiChannel.Ch1, 35, 23f);
        //Reset:
        MidiOut.SendNoteOn(MidiChannel.Ch1, 37, 20f);

        switch (mode) {
            case CameraMode.FRONTAL:
                MidiOut.SendNoteOn(MidiChannel.Ch1, 32, 21f);
                break;
            case CameraMode.ABOVE:
                MidiOut.SendNoteOn(MidiChannel.Ch1, 33, 21f);
                break;
            case CameraMode.SIDE:
                MidiOut.SendNoteOn(MidiChannel.Ch1, 34, 21f);
                break;
            case CameraMode.BELOW:
                MidiOut.SendNoteOn(MidiChannel.Ch1, 35, 21f);
                break;
            default:
                ResetKnobs();
                MidiOut.SendNoteOn(MidiChannel.Ch1, 32, 21f);
                break;
        }
    }

    private void ResetKnobs() {
        MidiOut.SendControlChange(MidiChannel.Ch1, 0x30, 63);
        MidiOut.SendControlChange(MidiChannel.Ch1, 0x31, 63);
        MidiOut.SendControlChange(MidiChannel.Ch1, 0x32, 1);
    }
}
