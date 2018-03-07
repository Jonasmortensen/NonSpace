using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneManager  {
    //void SetMainColor(Color color);
    void SetMainColorHue(float value);
    void SetMainColorSaturation(float value);
    //void SetSecondaryColor(Color color);
    void SetSecondaryColorHue(float value);
    void SetSecondaryColorSaturation(float value);
    void SetSpecialProperty1(float value);
    void SetSpecialProperty2(float value);
    void SetSpecialProperty3(float value);
    void SetSpecialProperty4(float value);
}
