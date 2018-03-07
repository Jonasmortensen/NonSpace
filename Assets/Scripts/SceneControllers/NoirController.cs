using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoirController : MonoBehaviour, ISceneController {
    private Light[] lights;
    private RainController rain;

    private Color CanvasColor;
    private Color SpotlightColor;
    private float RainAmount;
    private float uniqueProperty2;

    public void SetMainColor(Color color) {
        CanvasColor = color;
    }

    public void SetSecondaryColor(Color color) {
        SpotlightColor = color;
    }

    public void SetSpecialProperty1(float value) {
        RainAmount = value;
    }

    public void SetSpecialProperty2(float value) {
        uniqueProperty2 = value;
    }

    public Color GetMainColor() {
        return CanvasColor;
    }

    // Use this for initialization
    void Start () {
        GameObject canvas = transform.Find("Canvas").gameObject;
        rain = transform.GetComponentInChildren<RainController>();
        lights = GetComponentsInChildren<Light>();
        for(int i = 0; i < canvas.transform.childCount; i++) {
            var bgFader = canvas.transform.GetChild(i).gameObject.AddComponent<BackgroundFader>();
            bgFader.preset = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
        foreach(Light light in lights) {
            light.color = SpotlightColor;
        }

        rain.SetEmission(RainAmount);
	}

    public void SetSpecialProperty3(float value) {
        throw new NotImplementedException();
    }

    public void SetSpecialProperty4(float value) {
        throw new NotImplementedException();
    }

    public void SetMainColorHue(float value) {
        throw new NotImplementedException();
    }

    public void SetMainColorSaturation(float value) {
        throw new NotImplementedException();
    }

    public void SetSecondaryColorHue(float value) {
        throw new NotImplementedException();
    }

    public void SetSecondaryColorSaturation(float value) {
        throw new NotImplementedException();
    }
}
