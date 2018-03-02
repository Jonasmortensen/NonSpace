using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnMode {
    TRIANGLE, LINES, RANDOM, ULTRA_RANDOM
}

public class Spawner : MonoBehaviour {

    public GameObject prefab;
    public float spawnTime;
    public int objectLimit;
    public SpawnMode spawnMode;

    //private List<GameObject> live;
    //private List<GameObject> poolList;
    private GameObject modelPool;
    private GameObject liveModels;
    private int liveCount;
    private ISpawnProfile spawnProfile;

    //Grid order
    private static int[] gridOrder = { 4, 7, 6, 8, 3, 5, 1, 0, 2 };

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

    public bool spawn(SpawnDirection spawnDirection = SpawnDirection.CENTER) {
        if(liveCount >= objectLimit) {
            liveCount = objectLimit;
            return false;
        }

        Transform model = modelPool.transform.GetChild(0);
        model.position = spawnProfile.GetNextPosition(spawnDirection);
        model.transform.parent = liveModels.transform;
        liveCount++;

        return true;
    }

    public bool kill() {
        if(liveCount <= 0) {
            liveCount = 0;
            return false;
        }
        Transform toKill = liveModels.transform.GetChild(liveCount-1);
        toKill.parent = modelPool.transform;
        liveCount--;
        return true;
    }

    public void Clear() {
        while(kill());
        spawnProfile = new TriangleSP(3, 15);
    }

    public void Fill() {
        while (spawn());
    }
}
