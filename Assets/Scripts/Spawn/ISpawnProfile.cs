using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnDirection {
    LEFT, RIGHT, CENTER
}

public interface ISpawnProfile {
    Vector3 GetNextPosition(SpawnDirection direction);

}
