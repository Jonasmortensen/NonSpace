using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaporController : MonoBehaviour, ISceneController {
    private LandDeformer land;
    private Sun sun;
    private float sunHue;
    private float sunSaturation;
    private float floorHue;
    private float floorSaturation;

    public void SetMainColorHue(float value) {
        sunHue = value;
        updateSunColor();
    }

    public void SetMainColorSaturation(float value) {
        sunSaturation = value;
        updateSunColor();
    }

    private void updateSunColor() {
        Color col = Color.HSVToRGB(sunHue, sunSaturation, 1f);
        sun.SetColor(col);
    }

    public void SetSecondaryColorHue(float value) {
        floorHue = value;
    }

    public void SetSecondaryColorSaturation(float value) {
        floorSaturation = value;
    }

    public void SetSpecialProperty1(float value) {
        land.SetHeight(value * 50);
        land.SetMovement(1.0f - value);
        //Need to set sun movement of bars
    }

    public void SetSpecialProperty2(float value) {
        sun.SetSize(value);
    }

    public void SetSpecialProperty3(float value) {
        sun.transform.position = new Vector3(sun.transform.position.x, value - 60 + (value * 90), sun.transform.position.z);
    }

    public void SetSpecialProperty4(float value) {
        throw new NotImplementedException();
    }

    // Use this for initialization
    void Start () {
        land = GetComponentInChildren<LandDeformer>();
        sun = GetComponentInChildren<Sun>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
