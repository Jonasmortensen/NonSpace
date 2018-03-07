using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {
    public float speed;
    private Material mat;
    private float offset;
    private Light sunLight;


	// Use this for initialization
	void Start () {
        mat = GetComponent<Renderer>().material;
        sunLight = GetComponentInChildren<Light>();
	}

    public void SetColor(Color color) {
        mat.SetColor("_Emission", color);
    }

    public void SetSize(float value) {
        transform.localScale = new Vector3(value + 25 + value * 75, 0.1f, value + 25 + value * 75);
    }
	
	// Update is called once per frame
	void Update () {
        offset += Time.deltaTime * speed;
        mat.SetFloat("_Offset", offset);
    }
}
