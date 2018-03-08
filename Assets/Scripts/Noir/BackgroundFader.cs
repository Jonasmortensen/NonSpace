using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFader : MonoBehaviour {

    private Camera mainCam;

    private Light glow;
    private Renderer rend;

    public NoirManager preset;

	// Use this for initialization
	void Start () {
        glow = transform.GetComponentInChildren<Light>();
        rend = GetComponent<Renderer>();
        mainCam = GameController.Instance.MainCamera;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 wallOrientation = new Vector2(transform.up.x, transform.up.z);
        Vector2 camOrientation = new Vector2(mainCam.transform.forward.x, mainCam.transform.forward.z);
        float camRotIntensity = Mathf.Abs(Mathf.Sin((GameController.Instance.MainCamera.transform.localRotation.eulerAngles.x - 10) * Mathf.Deg2Rad));
        Debug.Log("intensity: " + camRotIntensity);

        float camRelativeIntensity = - Vector2.Dot(wallOrientation.normalized, camOrientation.normalized);
        camRelativeIntensity = (0.5f + camRelativeIntensity) * 0.6f;
        camRelativeIntensity = Mathf.Max(camRelativeIntensity, 0);
        camRelativeIntensity = camRelativeIntensity * (1 - camRotIntensity);
        camRelativeIntensity = Smoother.VerySmooth(camRelativeIntensity + camRotIntensity);
        glow.intensity = camRelativeIntensity * 5;
        glow.color = preset.GetMainColor();

        Color finalColor = preset.GetMainColor() * Mathf.LinearToGammaSpace(camRelativeIntensity);


        if (finalColor.r < 0.0001 && finalColor.g < 0.0001 && finalColor.b < 0.0001) {
            rend.enabled = false;
        } else if(rend.enabled == false) {
            rend.enabled = true;
        }

        rend.material.SetColor("_EmissionColor", finalColor);
    }
}
