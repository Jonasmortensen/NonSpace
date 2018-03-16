using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimator : MonoBehaviour {
    private Material mat;

    private float animationTime = 0.5f;
    private float elapsedTime;
    private float start;
    private float end;
    private bool isAnimating;
    private Action callback;

	// Update is called once per frame
	void Update () {
        if (isAnimating) {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationTime;
            float val = Mathf.Lerp(start, end, t);
            setAniamationStep(val);
            if (elapsedTime > animationTime) {
                isAnimating = false;
                setAniamationStep(end);
                if(callback != null) {
                    callback();
                }
                Destroy(this);
            }
        }
    }

    public void moveIn() {
        if(mat == null) {
            mat = transform.GetChild(0).GetComponent<Renderer>().material;
        }
        start = 0;
        setAniamationStep(start);
        end = 1;
        elapsedTime = 0;
        isAnimating = true;
    }

    public void moveOut(Action _callback) {
        if (mat == null) {
            mat = transform.GetChild(0).GetComponent<Renderer>().material;
        }
        start = 1;
        setAniamationStep(start);
        callback = _callback;
        end = 0;
        elapsedTime = 0;
        isAnimating = true;
    }

    private void setAniamationStep(float t) {
        mat.SetFloat("_CutHeight", t);
        mat.SetFloat("_Cutoff", 1 - t);
    }
}
