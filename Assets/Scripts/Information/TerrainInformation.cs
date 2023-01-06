using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TerrainInformation
{
    public GameObject terrainMesh;
    public GameObject[] platform = new GameObject[5];
    public GameObject[] enclosure = new GameObject[5];

    public TerrainInformation()
    {
        terrainMesh = GameObject.Find("Terrain");

        for (int i = 0; i < 5; i++)
        {
            platform[i] = GameObject.Find("TerrainPlatform" + i);
            enclosure[i] = GameObject.Find("TerrainEnclosure" + i);
        }
    }

    public TerrainInfo GetTerrainInformation()
    {
        TerrainInfo info = new TerrainInfo(terrainMesh, platform, enclosure);
        return info;
    }

    //This function works on square meshes 
    public float GetTerrainSideLength()
    {
        Mesh mesh = terrainMesh.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        
        //Make all vertices lie, so elevations are not accounted
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = 0;
        }
        
        var center = terrainMesh.transform.position;
        Vector3 furthestPoint;
        float currentDistance;
        float distance = 0;
        
        for (int i=0; i<vertices.Length; i++)
        {
            currentDistance = Vector3.Distance(center, vertices[i]);
            if (currentDistance > distance) { distance = currentDistance; furthestPoint = vertices[i]; }
        }
        
        //From half of diagonal to side length
        return (float)Math.Sqrt(2) * distance;
    }
}

public struct TerrainInfo
{
    public GameObject terrainMesh;
    public GameObject[] platform;
    public GameObject[] enclosure;

    public TerrainInfo(GameObject terrainMesh, GameObject[] platform, GameObject[] enclosure)
    {
        this.terrainMesh = terrainMesh;
        this.platform = platform;
        this.enclosure = enclosure;
    }
}
