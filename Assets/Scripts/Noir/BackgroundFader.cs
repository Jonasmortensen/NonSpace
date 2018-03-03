using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFader : MonoBehaviour {

    private Camera mainCam;

    private Light glow;
    private Renderer rend;

    public Color emissionColor;

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

        float AngleToScreen = Mathf.Max(- Vector2.Dot(wallOrientation.normalized, camOrientation.normalized), 0);
        //Debug.Log("Calculated wall vector: " + wallOrientation);
        //Debug.Log("Calculated cam vector: " + camOrientation);
        AngleToScreen = Smoother.VerySmooth(AngleToScreen);
        glow.intensity = AngleToScreen * 5;

        //Material mat = rend.material;

        Color finalColor = emissionColor * Mathf.LinearToGammaSpace(AngleToScreen);

        rend.material.SetColor("_EmissionColor", finalColor);



        Debug.Log("Calculated angle to screen: " + Math.Round(AngleToScreen, 2));
    }
}
