using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleSP : ISpawnProfile {

    private int modelCount;
    private bool[][] positions;
    private int currentIndex;
    private float size;


    public TriangleSP(float _size) {
        size = _size;
    }

    public Vector3 GetNextPosition(SpawnDirection direction) {
        Vector3 pos = indexToPosition(currentIndex);
        currentIndex++;
        return pos;
    }

    private Vector3 indexToPosition(int i) {
        int row = (int) ((-1 + Mathf.Sqrt(1 + 8 * i)) / 2);
        int triangleNumber = (row + 1) * (row) / 2;
        int column = i - triangleNumber;

        return new Vector3((0.5f * -row + column) * size, 0, row * size);
    }
}
