using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaporController : MonoBehaviour, IScenePreset {
    private LandDeformer land;
    private Sun sun;

    public void SetMainColor(Color color) {
        sun.SetColor(color);
    }

    public void SetSecondaryColor(Color color) {
        throw new NotImplementedException();
    }

    public void SetSpecialProperty1(float value) {
        land.SetHeight(value * 50);
        land.SetMovement(1.0f - value);
    }

    public void SetSpecialProperty2(float value) {
        sun.SetSize(value);
    }

    public void SetSpecialProperty3(float value) {
        throw new NotImplementedException();
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
