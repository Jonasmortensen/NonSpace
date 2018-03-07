using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnController {
    void SetSpawner(Spawner spawner);
    PlacementMode GetSelectedPlacementMode();
    PlaybackMode GetSelectedPlaybackMode();
    float GetMainHueValue();
    float GetMainSaturationValue();
    float GetSecondaryHueValue();
    float GetSecondarySarutaionValue();
    float GetSpecial1Value();
    float GetSpecial2Value();
    float GetSpecial3Value();
    float GetSpecial4Value();
}
