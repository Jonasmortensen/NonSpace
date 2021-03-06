﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlacementMode {
    TRIANGLE, RANDOM, LINES, ULTRARANDOM, RESET
}

public class Spawner : MonoBehaviour {

    public GameObject prefab;
    public int objectLimit;
    public PlacementMode spawnMode;

    private GameObject modelPool;
    private GameObject liveModels;
    private int liveCount;
    private ISpawnProfile spawnProfile;

    // Use this for initialization
    void Start () {
        //Create the pool for dead models
        modelPool = new GameObject();
        modelPool.name = "ModelPool";
        modelPool.transform.parent = transform;
        modelPool.transform.localPosition = Vector3.zero;
        modelPool.SetActive(false);

        //Create the go for live models
        liveModels = new GameObject();
        liveModels.name = "liveModels";
        liveModels.transform.parent = transform;
        liveModels.transform.localPosition = Vector3.zero;

        //Fill the modelPool
        for (int i = 0; i < objectLimit; i++) {
            GameObject currentModel = Instantiate(prefab);
            //currentModel.SetActive(false);
            currentModel.transform.parent = modelPool.transform;
        }

        spawnProfile = new TriangleSP(3, 15);
    }

    public bool Spawn(SpawnDirection spawnDirection = SpawnDirection.CENTER) {
        Vector3 nextPos = spawnProfile.GetNextPosition(spawnDirection);
        
        if(liveCount >= objectLimit) {
            Kill();
        }
        

        Transform model = modelPool.transform.GetChild(0);
        model.position = nextPos;
        SpawnAnimator currentAnimator = model.GetComponent<SpawnAnimator>();
        if (currentAnimator != null) {
            Destroy(currentAnimator);
        }
        model.gameObject.AddComponent<SpawnAnimator>().moveIn();
        model.transform.parent = liveModels.transform;
        liveCount++;

        return true;
    }

    public bool Kill() {
        if(liveCount <= 0) {
            liveCount = 0;
            return false;
        }

        Transform toKill = liveModels.transform.GetChild(liveCount-1);
        SpawnAnimator currentAnimator = toKill.GetComponent<SpawnAnimator>();
        if (currentAnimator != null) {
            Destroy(currentAnimator);
        }

        Action callback = delegate () {
            toKill.parent = modelPool.transform;
        };
        liveCount--;
        toKill.gameObject.AddComponent<SpawnAnimator>().moveOut(callback);
        return true;
    }

    public void Clear() {
        for(int i = liveCount; i > 0; i--) {
            Kill();
        }
        spawnProfile = new TriangleSP(3, 15);
    }

    public void Fill() {
        for (int i = liveCount; i < objectLimit; i++) {
            Spawn();
        }
    }
}
