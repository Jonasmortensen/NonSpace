using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraController {
    void SetCameraManager(CameraManager camManager);
    float GetWorldRotation();
    float GetCameraRotation();
    CameraMode GetSelectedCameraMode();
}
