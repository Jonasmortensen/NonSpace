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
        ListenPlaybackInput();
        ListenPlacementInput();
        ListenSpawnInput();

    }

    public void InitController() {
        Debug.Log("Initialising!");
        TurnOffLights();
        InitiateKnobs();
        SetCameraLights(CameraMode.FRONTAL);;
        SetPlaybackLights(PlaybackMode.REAL_TIME);
        SetPlacementLights(PlacementMode.TRIANGLE);
        InitSpawnLights();
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

    private void ResetKnobs() {
        MidiOut.SendControlChange(MidiChannel.Ch1, 0x30, 63);
        MidiOut.SendControlChange(MidiChannel.Ch1, 0x31, 63);
        MidiOut.SendControlChange(MidiChannel.Ch1, 0x32, 1);
    }

    public void TurnOffLights() {
        for (int i = 0; i < 39; i++) {
            MidiOut.SendNoteOff(MidiChannel.Ch1, i);
        }

        MidiOut.SendNoteOff(MidiChannel.Ch1, 51);
    }



#region Camera
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
    #endregion

#region Playback
    private void ListenPlaybackInput() {
        if (MidiInput.GetKeyDown(MidiChannel.Ch1, 24)) {
            SetPlaybackLights(PlaybackMode.REAL_TIME);
        }
        if (MidiInput.GetKeyDown(MidiChannel.Ch1, 25)) {
            SetPlaybackLights(PlaybackMode.SHIFTED);
        }
        if (MidiInput.GetKeyDown(MidiChannel.Ch1, 26)) {
            SetPlaybackLights(PlaybackMode.BACKNFORTH);
        }
        if (MidiInput.GetKeyDown(MidiChannel.Ch1, 27)) {
            SetPlaybackLights(PlaybackMode.GLITCH);
        }
        if (MidiInput.GetKeyDown(MidiChannel.Ch1, 29)) {
            SetPlaybackLights(PlaybackMode.RESET);
        }
    }

    public void SetPlaybackLights(PlaybackMode mode) {
        MidiOut.SendNoteOn(MidiChannel.Ch1, 24, 15f);
        MidiOut.SendNoteOn(MidiChannel.Ch1, 25, 15f);
        MidiOut.SendNoteOn(MidiChannel.Ch1, 26, 15f);
        MidiOut.SendNoteOn(MidiChannel.Ch1, 27, 15f);
        //Reset:
        MidiOut.SendNoteOn(MidiChannel.Ch1, 29, 16f);

        switch (mode) {
            case PlaybackMode.REAL_TIME:
                MidiOut.SendNoteOn(MidiChannel.Ch1, 24, 13f);
                break;
            case PlaybackMode.SHIFTED:
                MidiOut.SendNoteOn(MidiChannel.Ch1, 25, 13f);
                break;
            case PlaybackMode.BACKNFORTH:
                MidiOut.SendNoteOn(MidiChannel.Ch1, 26, 13f);
                break;
            case PlaybackMode.GLITCH:
                MidiOut.SendNoteOn(MidiChannel.Ch1, 27, 13f);
                break;
            default:
                MidiOut.SendNoteOn(MidiChannel.Ch1, 24, 13f);
                break;
        }
    }
    #endregion

#region Placement
    private void ListenPlacementInput() {
        if (MidiInput.GetKeyDown(MidiChannel.Ch1, 16)) {
            SetPlacementLights(PlacementMode.TRIANGLE);
        }
        if (MidiInput.GetKeyDown(MidiChannel.Ch1, 17)) {
            SetPlacementLights(PlacementMode.RANDOM);
        }
        if (MidiInput.GetKeyDown(MidiChannel.Ch1, 18)) {
            SetPlacementLights(PlacementMode.LINES);
        }
        if (MidiInput.GetKeyDown(MidiChannel.Ch1, 19)) {
            SetPlacementLights(PlacementMode.ULTRARANDOM);
        }
        if (MidiInput.GetKeyDown(MidiChannel.Ch1, 21)) {
            SetPlacementLights(PlacementMode.RESET);
        }
    }

    public void SetPlacementLights(PlacementMode mode) {
        MidiOut.SendNoteOn(MidiChannel.Ch1, 16, 7f);
        MidiOut.SendNoteOn(MidiChannel.Ch1, 17, 7f);
        MidiOut.SendNoteOn(MidiChannel.Ch1, 18, 7f);
        MidiOut.SendNoteOn(MidiChannel.Ch1, 19, 7f);
        //Reset:
        MidiOut.SendNoteOn(MidiChannel.Ch1, 21, 8f);

        switch (mode) {
            case PlacementMode.TRIANGLE:
                MidiOut.SendNoteOn(MidiChannel.Ch1, 16, 5f);
                break;
            case PlacementMode.RANDOM:
                MidiOut.SendNoteOn(MidiChannel.Ch1, 17, 5f);
                break;
            case PlacementMode.LINES:
                MidiOut.SendNoteOn(MidiChannel.Ch1, 18, 5f);
                break;
            case PlacementMode.ULTRARANDOM:
                MidiOut.SendNoteOn(MidiChannel.Ch1, 19, 5f);
                break;
            default:
                MidiOut.SendNoteOn(MidiChannel.Ch1, 16, 5f);
                break;
        }
    }
    #endregion

#region Spawn
    private void ListenSpawnInput() {
        if (MidiInput.GetKeyDown(MidiChannel.Ch1, 8)) {
            PressSpawnLight(SpawnDirection.LEFT);
        }
        if (MidiInput.GetKeyDown(MidiChannel.Ch1, 9)) {
            PressSpawnLight(SpawnDirection.CENTER);
        }
        if (MidiInput.GetKeyDown(MidiChannel.Ch1, 10)) {
            PressSpawnLight(SpawnDirection.RIGHT);
        }
        if (MidiInput.GetKeyDown(MidiChannel.Ch1, 11)) {
            PressSpawnLight(SpawnDirection.FILL);
        }
        if (MidiInput.GetKeyDown(MidiChannel.Ch1, 13)) {
            PressSpawnLight(SpawnDirection.CLEAR);
        }
    }
    private void InitSpawnLights() {
        MidiOut.SendNoteOn(MidiChannel.Ch1, 8, 34f);
        MidiOut.SendNoteOn(MidiChannel.Ch1, 9, 34f);
        MidiOut.SendNoteOn(MidiChannel.Ch1, 10, 34f);
        MidiOut.SendNoteOn(MidiChannel.Ch1, 11, 34f);
        //Reset:
        MidiOut.SendNoteOn(MidiChannel.Ch1, 13, 36f);
    }

    private void PressSpawnLight(SpawnDirection direction) {
        switch (direction) {
            case SpawnDirection.LEFT:
                MidiOut.SendNoteOn(MidiChannel.Ch1, 8, 0f);
                MidiOut.SendNoteOn(MidiChannel.Ch5, 8, 34f);
                break;
            case SpawnDirection.CENTER:
                MidiOut.SendNoteOn(MidiChannel.Ch1, 9, 0f);
                MidiOut.SendNoteOn(MidiChannel.Ch5, 9, 34f);
                break;
            case SpawnDirection.RIGHT:
                MidiOut.SendNoteOn(MidiChannel.Ch1, 10, 0f);
                MidiOut.SendNoteOn(MidiChannel.Ch5, 10, 34f);
                break;
            case SpawnDirection.FILL:
                MidiOut.SendNoteOn(MidiChannel.Ch1, 11, 0f);
                MidiOut.SendNoteOn(MidiChannel.Ch5, 11, 34f);
                break;
            case SpawnDirection.CLEAR:
                MidiOut.SendNoteOn(MidiChannel.Ch1, 13, 0f);
                MidiOut.SendNoteOn(MidiChannel.Ch5, 13, 36f);
                break;
        }
    }
#endregion

}
