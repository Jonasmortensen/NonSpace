using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSP : ISpawnProfile
{

    private Vector2 size;
    private int models;
    private int maxModels;
    private float leftX;
    private float rightX;
    private float xStep;

    public RandomSP(Vector2 _size, int max) {
        size = _size;
        maxModels = max;
        xStep = _size.x / max;
    }

    public Vector3? GetNextPosition(SpawnDirection direction) {
        float z = Random.Range(0, size.y);
        float x = 0;
        switch (direction) {
            case SpawnDirection.CENTER:
                x = leftX < rightX ? -size.x / 2 + leftX : size.x / 2 - rightX;
                break;
            case SpawnDirection.LEFT:
                x = (-size.x) / 2 + leftX;
                leftX += xStep;
                break;
            case SpawnDirection.RIGHT:
                x = size.x / 2 - rightX;
                rightX += xStep;
                break;
            default:
                break;
        }
        return null;
    }
}
