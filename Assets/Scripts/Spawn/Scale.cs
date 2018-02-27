using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour {

    private Transform afterParent;
    private Vector3 startScale;
    private Vector3 goalScale;
    private float elapsedTime;
    private float scaleTime;
    private bool moving;

    // Update is called once per frame
    void Update () {
        if(moving) {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / scaleTime;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            transform.localScale = Vector3.Lerp(startScale, goalScale, t);
            if(elapsedTime > scaleTime) {
                //Happens if moving out
                if(afterParent != null) {
                    transform.parent = afterParent;
                    gameObject.SetActive(false);
                    transform.localScale = startScale;
                }
                Destroy(this);
            }
        }
	}

    public void moveIn(float time, Transform goalParent) {
        if (moving)
            return;
        scaleTime = time;
        elapsedTime = 0;

        startScale = Vector3.zero;
        goalScale = transform.localScale;
        transform.localScale = Vector3.zero;

        transform.parent = goalParent;
        gameObject.SetActive(true);

        moving = true;
    }

    public void moveOut(float time, Transform goalParent) {
        if (moving)
            return;
        scaleTime = time;
        elapsedTime = 0;

        goalScale = Vector3.zero;
        startScale = transform.localScale;

        afterParent = goalParent;

        moving = true;
    }
}
