using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandDeformer : MonoBehaviour {

    public Texture2D noiseTexture;
    private float height;
    private float scale = 0.5f;
    public AnimationCurve valley;
    [Range(0, 50)]
    public float heightSlider;
    [Range(0, 1)]
    public float movementSlider;
    private float waveAmount;
    private float waveSpeed;
    private float offsetPrSecond;
    private float minX;
    private float maxX;
    private float yOffset;

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
        SetHeight(heightSlider);
        SetMovement(movementSlider);
        UpdateMesh();
    }

    public void SetHeight(float _height) {
        height = _height;
    }

    public void SetMovement(float movement) {
        waveAmount = 0.3f * movement;
        offsetPrSecond = 0.5f * movement;
    }

    void UpdateMesh() {
        Mesh sourceMesh = meshFilter.mesh;
        yOffset += offsetPrSecond * Time.deltaTime;
        
        Vector3[] vertices = sourceMesh.vertices;
        for (int i = 0; i < vertices.Length; i++) {
            Vector3 vertex = vertices[i];
            float t = (vertex.x - minX) / (maxX - minX);

            //float textureSample = noiseTexture.GetPixel((int)(vertex.x * scale) % noiseTexture.width, (int)(vertex.y * scale + yOffset) % noiseTexture.height).r;
            float textureSample = Mathf.PerlinNoise((vertex.x * scale), (vertex.y * scale + yOffset));


            float sineValue = Mathf.Sin(vertex.x + Time.time * waveSpeed);
            vertex.z = (textureSample * height + sineValue * waveAmount * height) * valley.Evaluate(t);
            vertices[i] = vertex;
        }
        

        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.RecalculateBounds();
        NormalSolver.RecalculateNormals(meshFilter.mesh, 0);
    }

}
