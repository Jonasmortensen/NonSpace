using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnDirection {
    LEFT, CENTER, RIGHT, FILL, CLEAR
}

public interface ISpawnProfile {
    Vector3? GetNextPosition(SpawnDirection direction);
}
