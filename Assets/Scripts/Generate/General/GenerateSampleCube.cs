using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenerateSampleCube
{
    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    GameObject meshObject;
    MeshCollider meshCollider;

    private Vector3 centerPrevious = new Vector3(0,0,0);
    private MeshDisplay display;
    private AssignMaterials assignMaterials;

    public void DecideOnGenerateMesh(Vector3 inputPoint)
    {
        WorldSettings settings = GameObject.FindObjectOfType<WorldSettings>();
        HoldInformation holdInformation = GameObject.FindObjectOfType<HoldInformation>();
        assignMaterials = GameObject.FindObjectOfType<AssignMaterials>();

        float sampleSize = holdInformation.terrainLength/settings.sampleSize;

        Vector3 center = new Vector3(sampleSize * (float)Math.Round(inputPoint.x / sampleSize), sampleSize * (float)Math.Round(inputPoint.y / sampleSize), sampleSize * (float)Math.Round(inputPoint.z / sampleSize));

        int isNew = 0;

        //Find if the cube is new one, or not
        for (int i=0; i<3; i++)
        {
            if (center[i] != this.centerPrevious[i]) { isNew = 1; }
        }

        //Check if the new cube is not outside of the bounds
        for (int i = 0; i < 3; i++)
        {
            if (center[i] > holdInformation.terrainLength/2) { isNew = 2; }
        }

        //Check if the Cube is under terrain
        if (IsUnderTerrain(holdInformation.terrainInformation.terrainMesh.GetComponent<MeshFilter>().mesh.vertices, center, sampleSize)) { isNew = 2; }

        centerPrevious = center;

        //Cube is new and in boundaries, destroy previous generate new
        if (isNew == 1)
        {
            MapDestroy.DestroyMesh("SampleCube");
            //GenerateMesh(sampleSize);
            MeshData mesh = GenerateShapes.GenerateCube(center, sampleSize);
            Material material = assignMaterials.sampleCubeMaterial;
            display = GameObject.FindObjectOfType<MeshDisplay>();
            display.DrawMesh(mesh, material, "SampleCube");
        }
        //Cube is new, but outside boundaries, destrou previous but do not generate new
        else if (isNew == 2)
        {
            MapDestroy.DestroyMesh("SampleCube");
        }
    }

    public void GenerateMesh(float sampleSize)
    {
        Vector3 center = centerPrevious;
        Vector3[] vertices = new Vector3[8];

        //Adding individual vertices
        vertices[0] = new Vector3(center.x - sampleSize / 2, center.y - sampleSize / 2, center.z - sampleSize / 2);
        vertices[1] = new Vector3(center.x + sampleSize / 2, center.y - sampleSize / 2, center.z - sampleSize / 2);
        vertices[2] = new Vector3(center.x + sampleSize / 2, center.y + sampleSize / 2, center.z - sampleSize / 2);
        vertices[3] = new Vector3(center.x - sampleSize / 2, center.y + sampleSize / 2, center.z - sampleSize / 2);
        vertices[4] = new Vector3(center.x - sampleSize / 2, center.y + sampleSize / 2, center.z + sampleSize / 2);
        vertices[5] = new Vector3(center.x + sampleSize / 2, center.y + sampleSize / 2, center.z + sampleSize / 2);
        vertices[6] = new Vector3(center.x + sampleSize / 2, center.y - sampleSize / 2, center.z + sampleSize / 2);
        vertices[7] = new Vector3(center.x - sampleSize / 2, center.y - sampleSize / 2, center.z + sampleSize / 2);

        //Adding individual triangles
        int[] triangles = {
            0,2,1,
            0,3,2,
            2,3,4,
            2,4,5,
            1,2,5,
            1,5,6,
            0,7,4,
            0,4,3,
            5,4,7,
            5,7,6,
            0,6,7,
            0,1,6
        };

        MeshData cube = new MeshData(8, 1);
        cube.vertices = vertices;
        cube.triangles = triangles;

        VisualiseMesh(cube);
    }

    public void VisualiseMesh(MeshData meshData)
    {
        AssignMaterials assign = GameObject.FindObjectOfType<AssignMaterials>();
        Material material = assign.sampleCubeMaterial;

        meshObject = new GameObject("SampleCube");
        meshRenderer = meshObject.AddComponent<MeshRenderer>();
        meshFilter = meshObject.AddComponent<MeshFilter>();
        meshCollider = meshObject.AddComponent(typeof(MeshCollider)) as MeshCollider;

        meshRenderer.material = material;
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshCollider.sharedMesh = meshData.CreateMesh();
    }

    public bool IsUnderTerrain(Vector3[] vertices, Vector3 cubeCenter, float sideLength)
    {
        float length;
        float shortestLength = float.PositiveInfinity; // Returns infinity
        Vector3 closestVertice = new Vector3(0,0,0);

        Vector3 upperCenter = new Vector3(cubeCenter.x, cubeCenter.y + sideLength/2, cubeCenter.z);

        //Find the closest vertice to the center of the top
        for (int i=0; i<vertices.Length; i++)
        {
            length = Vector3.Distance(upperCenter, vertices[i]);
            if (length < shortestLength) { shortestLength = length; closestVertice = vertices[i]; }
        }

        if (closestVertice.y < upperCenter.y) { return false; }
        else { return true; }
    }
}
