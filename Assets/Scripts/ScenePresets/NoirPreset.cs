using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoirPreset : MonoBehaviour, IScenePreset {
    //private GameObject canvas;
    private Light[] lights;
    private RainController rain;

    public Color mainColor;
    public Color secondayColor;
    [Range(0, 1)]
    public float uniqueProperty1;
    private float uniqueProperty2;

    public void SetMainColor(Color color) {
        mainColor = color;
    }

    public void SetSecondaryColor(Color color) {
        secondayColor = color;
    }

    public void SetUniqueProperty1(float value) {
        uniqueProperty1 = value;
    }

    public void SetUniqueProperty2(float value) {
        uniqueProperty2 = value;
    }

    public Color GetMainColor() {
        return mainColor;
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
            light.color = secondayColor;
        }

        rain.SetEmission(uniqueProperty1);
	}
}
