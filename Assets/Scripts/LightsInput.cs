﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsInput : MonoBehaviour {
    [Range(0, 1)]
    public float MainHue;
    [Range(0, 1)]
    public float MainSaturation;
    [Range(0, 1)]
    public float SecondaryHue;
    [Range(0, 1)]
    public float SecondarySaturation;
    [Range(0, 1)]
    public float Special1;
    [Range(0, 1)]
    public float Special2;
    [Range(0, 1)]
    public float Special3;
    [Range(0, 1)]
    public float Special4;
    public GameObject SceneControllerObject;
    private ISceneManager SceneController;

	// Use this for initialization
	void Start () {
        SceneController = SceneControllerObject.GetComponent<ISceneManager>();
	}
	
	// Update is called once per frame
	void Update () {
        SceneController.SetMainColorHue(MainHue);
        SceneController.SetMainColorSaturation(1-MainSaturation);
        SceneController.SetSecondaryColorHue(SecondaryHue);
        SceneController.SetSecondaryColorSaturation(1-SecondarySaturation);
        SceneController.SetSpecialProperty1(Special1);
        SceneController.SetSpecialProperty2(Special2);
        SceneController.SetSpecialProperty3(Special3);
    }

}
