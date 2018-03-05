using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsInput : MonoBehaviour {
    public Color PrimaryColor;
    public Color SecondaryColor;
    [Range(0, 1)]
    public float Special1;
    [Range(0, 1)]
    public float Special2;
    [Range(0, 1)]
    public float Special3;
    [Range(0, 1)]
    public float Special4;
    public GameObject SceneController;
    private IScenePreset ScenePreset;

	// Use this for initialization
	void Start () {
        ScenePreset = SceneController.GetComponent<VaporController>();
	}
	
	// Update is called once per frame
	void Update () {
        ScenePreset.SetMainColor(PrimaryColor);
        //ScenePreset.SetSecondaryColor(SecondaryColor);
        ScenePreset.SetSpecialProperty1(Special1);
        ScenePreset.SetSpecialProperty2(Special2);
    }
}
