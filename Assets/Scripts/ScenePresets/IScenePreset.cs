using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScenePreset  {
    void SetMainColor(Color color);
    void SetSecondaryColor(Color color);
    void SetUniqueProperty1(float value);
    void SetUniqueProperty2(float value);
}
