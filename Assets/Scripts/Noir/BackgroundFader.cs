using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFader : MonoBehaviour {

    private Camera mainCam;

    private Light glow;
    private Renderer rend;

    public NoirController preset;

	// Use this for initialization
	void Start () {
        glow = transform.GetComponentInChildren<Light>();
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Transform cam = GameController.Instance.MainCamera.transform;
        Vector2 wallOrientation = new Vector2(transform.up.x, transform.up.z);
        Vector2 camOrientation = new Vector2(cam.forward.x, cam.forward.z);


        float camRelativeIntensity = - Vector2.Dot(wallOrientation.normalized, camOrientation.normalized);
        camRelativeIntensity = (0.5f + camRelativeIntensity) * 0.6f;
        camRelativeIntensity = Mathf.Max(camRelativeIntensity, 0);
        camRelativeIntensity = Smoother.VerySmooth(camRelativeIntensity);
        glow.intensity = camRelativeIntensity * 5;
        glow.color = preset.GetMainColor();

        Color finalColor = preset.GetMainColor() * Mathf.LinearToGammaSpace(camRelativeIntensity);


        if (finalColor.r == 0 && finalColor.g == 0 && finalColor.b == 0) {
            rend.enabled = false;
        } else if(rend.enabled == false) {
            rend.enabled = true;
        }

        rend.material.SetColor("_EmissionColor", finalColor);
    }
}
