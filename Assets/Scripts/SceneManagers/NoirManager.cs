using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoirManager : MonoBehaviour, ISceneManager {
    private Light[] lights;
    private RainController rain;

    public Color CanvasColor;
    public Color SpotlightColor;
    private float mainHue;
    private float mainSat;
    private float mainVal;
    private float secHue;
    private float secSat;
    private float secVal;
    private float RainAmount;

    public void SetMainColorHue(float value) {
        mainHue = value;
    }

    public void SetMainColorSaturation(float value) {
        mainSat = 1 - value;
    }

    public void SetSecondaryColorHue(float value) {
        mainVal = value;
    }

    public void SetSecondaryColorSaturation(float value) {
        secHue = value;
    }

    public void SetSpecialProperty1(float value) {
        secSat = 1 - value;
    }

    public void SetSpecialProperty2(float value) {
        secVal = value;
    }

    public void SetSpecialProperty3(float value) {
        RainAmount = value;
    }

    public void SetSpecialProperty4(float value) {
        //throw new NotImplementedException();
    }

    public Color GetMainColor() {
        return CanvasColor;
    }

    // Use this for initialization
    void Start () {
        GameObject canvas = transform.Find("Canvas").gameObject;
        rain = transform.GetComponentInChildren<RainController>();
        mainSat = 0.5f;
        mainVal = 1.0f;
        //lights = GetComponentsInChildren<Light>();
        for(int i = 0; i < canvas.transform.childCount; i++) {
            var bgFader = canvas.transform.GetChild(i).gameObject.AddComponent<BackgroundFader>();
            bgFader.preset = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
        UpdateColors();
        /*
        foreach(Light light in lights) {
            light.color = SpotlightColor;
        }
        */

        rain.SetEmission(RainAmount);
	}

    private void UpdateColors() {
        CanvasColor = Color.HSVToRGB(mainHue, mainSat, mainVal);
        SpotlightColor = Color.HSVToRGB(secHue, secSat, secVal);
    }
}
