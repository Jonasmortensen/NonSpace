using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandDeformer : MonoBehaviour {

    public Texture2D noiseTexture;
    public float height;
    public float scale;
    public AnimationCurve valley;
    public float waveAmount;
    public float waveSpeed;
    private float minX;
    private float maxX;

    private MeshFilter meshFilter;


	// Use this for initialization
	void Start () {
        meshFilter = GetComponent<MeshFilter>();
        //Delete shared vertices in the plane
        Mesh sourceMesh = meshFilter.mesh;

        int[] sourceIndices = sourceMesh.GetTriangles(0);
        Vector3[] sourceVerts = sourceMesh.vertices;
        Vector2[] sourceUVs = sourceMesh.uv;

        int[] newIndices = new int[sourceIndices.Length];
        Vector3[] newVertices = new Vector3[sourceIndices.Length];
        Vector2[] newUVs = new Vector2[sourceIndices.Length];

        // Create a unique vertex for every index in the original Mesh:
        for (int i = 0; i < sourceIndices.Length; i++) {
            newIndices[i] = i;
            newVertices[i] = sourceVerts[sourceIndices[i]];
            newUVs[i] = sourceUVs[sourceIndices[i]];
        }

        Mesh unsharedVertexMesh = new Mesh();
        unsharedVertexMesh.vertices = newVertices;
        unsharedVertexMesh.uv = newUVs;

        unsharedVertexMesh.SetTriangles(newIndices, 0);
        meshFilter.mesh = unsharedVertexMesh;
        meshFilter.mesh.RecalculateBounds();
        //Call the update to apply fractal texture

        minX = meshFilter.mesh.bounds.min.x;
        maxX = meshFilter.mesh.bounds.max.x;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateMesh();
    }

    void UpdateMesh() {
        Mesh sourceMesh = meshFilter.mesh;
        
        Vector3[] vertices = sourceMesh.vertices;
        for (int i = 0; i < vertices.Length; i++) {
            Vector3 vertex = vertices[i];
            float t = (vertex.x - minX) / (maxX - minX);
            float textureSample = noiseTexture.GetPixel((int)(vertex.x * scale), (int)(vertex.y * scale)).r;
            float sineValue = Mathf.Sin(vertex.x * 0.5f + Time.time * waveSpeed);
            vertex.z = (textureSample * height + sineValue * waveAmount) * valley.Evaluate(t);
            vertices[i] = vertex;
        }
        

        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.RecalculateBounds();
        NormalSolver.RecalculateNormals(meshFilter.mesh, 0);
    }

}
