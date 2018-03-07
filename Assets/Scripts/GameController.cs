using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Camera MainCamera;
    public APC40Controller controller;
    public GameObject sceneManager;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        controller.SetSceneManager(sceneManager.GetComponent<ISceneManager>());
    }

    public static GameController Instance { get; private set; }
}