using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Camera MainCamera;
    public CameraManager cameraManager;
    public GameObject sceneManager;
    public APC40Controller controller;

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
        controller.SetCameraManager(cameraManager);
    }

    public static GameController Instance { get; private set; }
}