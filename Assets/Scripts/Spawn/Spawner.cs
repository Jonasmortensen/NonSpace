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
        //poolList = new List<GameObject>();
        //live = new List<GameObject>();

        modelPool = new GameObject();
        modelPool.name = "ModelPool";
        modelPool.transform.parent = transform;
        modelPool.transform.localPosition = Vector3.zero;

        liveModels = new GameObject();
        liveModels.name = "liveModels";
        liveModels.transform.parent = transform;
        liveModels.transform.localPosition = Vector3.zero;

        for (int i = 0; i < objectLimit; i++) {
            GameObject currentModel = Instantiate(prefab);
            currentModel.SetActive(false);
            currentModel.transform.parent = modelPool.transform;
        }

        spawnProfile = new TriangleSP(3, 15);
    }
	
	// Update is called once per frame
	void Update () {
        /*
        if(Input.GetKeyDown(KeyCode.Q)) {
            spawn();
        }


        if (Input.GetKeyDown(KeyCode.W)) {
            kill();
        }


        if(Input.GetKey(KeyCode.E)) {
            killAll();
        }
        */
    }

    public bool spawn(SpawnDirection spawnDirection) {
        if(liveCount >= objectLimit) {
            liveCount = objectLimit;
            return false;
        }

        Transform model = modelPool.transform.GetChild(0);
        model.position = spawnProfile.GetNextPosition(spawnDirection);
        Scale scaler = model.gameObject.AddComponent<Scale>();
        scaler.moveIn(1f, liveModels.transform);
        liveCount++;

        return true;
    }

    public bool kill() {
        if(liveCount <= 0) {
            liveCount = 0;
            return false;
        }
        Debug.Log("Killing: " + liveCount);
        Transform toKill = liveModels.transform.GetChild(liveCount-1);
        Scale scaler = toKill.gameObject.AddComponent<Scale>();
        scaler.moveOut(1f, modelPool.transform);
        liveCount--;
        return true;
    }

    public void killAll() {
        for(int i = 0; i < liveCount; i++) {
            kill();
        }
    }

    public static Vector3 GetSpawnPosition(SpawnMode mode, int i) {
        switch (mode) {
            case SpawnMode.TRIANGLE:
                return new Vector3(gridOrder[i] % 3 * 3 - 3, 0, gridOrder[i] / 3 * 3 - 3);
        }
        return Vector3.zero;

    }


}
