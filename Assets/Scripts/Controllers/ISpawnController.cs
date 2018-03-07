using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnController {
    void SetSpawner(Spawner spawner);
    PlacementMode GetSelectedPlacementMode();
}
