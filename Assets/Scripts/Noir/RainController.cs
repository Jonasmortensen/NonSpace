using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour {

    private ParticleSystem[] rainSystems;
    private float[] emissions;

	// Use this for initialization
	void Start () {
        ParticleSystem[] childSystems = transform.GetComponentsInChildren<ParticleSystem>();

        rainSystems = new ParticleSystem[childSystems.Length + 1];
        emissions = new float[rainSystems.Length];

        rainSystems[0] = GetComponent<ParticleSystem>();
        emissions[0] = rainSystems[0].emission.rateOverDistanceMultiplier;
        for(int i = 1; i < rainSystems.Length; i++) {
            rainSystems[i] = childSystems[i - 1];
            emissions[i] = rainSystems[i].emission.rateOverTimeMultiplier;
        }

	}

    public void SetEmission(float t) {
        for(int i = 0; i < rainSystems.Length; i++) {
            var emission = rainSystems[i].emission;
            emission.rateOverTimeMultiplier = emissions[i] * t;
        }
    }
}
