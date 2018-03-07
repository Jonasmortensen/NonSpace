using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Smoother{
    public static float VerySmooth(float t) {
        return t * t * t * (t * (6f * t - 15f) + 10f);
    }
	
}
