using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is hardcoded to 15!
public class TriangleSP : ISpawnProfile {
    private int models;
    private int maxModels;
    private int numOfRows;
    //private int currentIndex;
    private float size;
    private bool[] occupations;

    private static int[] CenterOrder = { 4, 7, 8, 12, 3, 5, 6, 9, 11, 13, 0, 1, 2, 10, 14 };
    private static int[] LeftOrder = { 10, 6, 11, 3, 7, 1, 12, 4, 0, 8, 2, 13, 5, 9 , 14 };
    private static int[] RightOrder = { 14, 9, 13, 5, 8, 2, 12, 4, 0, 7, 1, 11, 3, 6, 10 };

    public TriangleSP(float _size, int modelCount) {
        size = _size;
        maxModels = modelCount;
        numOfRows = (int)((-1 + Mathf.Sqrt(1 + 8 * modelCount)) / 2);
        occupations = new bool[modelCount];
    }

    public void decrementModels() {
        models--;
    }

    public void incrementModels() {
        models++;
    }

    //TODO: THIS IS PRETTY SLOW
    public Vector3 GetNextPosition(SpawnDirection direction) {
        if(models >= maxModels) {
            occupations = new bool[maxModels];
            models = 0;
        }
        int indexResult;
        int[] order;
        switch (direction) {
            case SpawnDirection.CENTER:
                order = CenterOrder;
                break;
            case SpawnDirection.LEFT:
                order = LeftOrder;
                break;
            case SpawnDirection.RIGHT:
                order = RightOrder;
                break;
            default:
                order = CenterOrder;
                break;
        }

        int i = 0;
        indexResult = order[i];
        while(occupations[indexResult]) {
            i++;
            indexResult = order[i];
        }

        Vector3 pos = indexToPosition(indexResult);
        models++;
        return pos;
    }


    private Vector3 indexToPosition(int i) {
        Debug.Log("Index pos: " + i);
        int row = (int) ((-1 + Mathf.Sqrt(1 + 8 * i)) / 2);
        int triangleNumber = (row + 1) * (row) / 2;
        int column = i - triangleNumber;
        occupations[i] = true;

        return new Vector3((0.5f * -row + column) * size, 2, (row - (numOfRows / 2)) * size);
    }
}
