using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderGenerator
{
    public static MeshData[] GenerateMesh(MeshData terrainMesh, int meshDimensions, float height)
    {
        MeshData[] meshData = new MeshData[5];
        Vector3[] terrainVertices = new Vector3[meshDimensions];

        for (int i=0; i<4; i++)
        {
            if (i == 0)
            {
                terrainVertices = terrainMesh.vertices[0..meshDimensions];
            }
            else if (i == 1)
            {
                int k = 0;
                for (int j = 0; j < meshDimensions * meshDimensions; j++)
                {
                    if (j % meshDimensions == 0)
                    {
                        terrainVertices[k] = terrainMesh.vertices[j];
                        k++;
                    }
                }
            }
            else if (i == 2)
            {
                int k = 0;
                for (int j = 0; j < meshDimensions * meshDimensions; j++)
                {
                    if (j % meshDimensions == 0)
                    {
                        if (k < meshDimensions)
                        {
                            terrainVertices[k] = terrainMesh.vertices[j + meshDimensions - 1];
                            k++;
                        }
                    }
                }
            }
            else
            {
                terrainVertices = terrainMesh.vertices[(meshDimensions * meshDimensions - meshDimensions)..(meshDimensions * meshDimensions)];
            }
            meshData[i] = GenerateMeshWall(terrainMesh, meshDimensions, terrainVertices, height);
        }

        meshData[4] = GenerateMeshPlatform(terrainMesh, meshDimensions, height);

        return meshData;
    }

    public static MeshData GenerateMeshWall(MeshData terrainMesh, int meshDimensions, Vector3[] terrainVertices, float height)
    {
        MeshData Wall = new MeshData(meshDimensions*2, 1);
        Vector3[] platformVertices = new Vector3[meshDimensions];

        for (int i = 0; i < meshDimensions; i++)
        {
            platformVertices[i] = new Vector3(terrainVertices[i].x, height, terrainVertices[i].z);
        }

        for (int y = 0; y<2; y++)
        {
            for (int x=0; x<meshDimensions; x++)
            {
                Wall.uvs[(y+1)*x] = new Vector2(x / (float)meshDimensions, y / (float)2);
            }
        }

        var vertices = new Vector3[terrainVertices.Length + platformVertices.Length];
        terrainVertices.CopyTo(vertices, 0);
        platformVertices.CopyTo(vertices, terrainVertices.Length);

        Wall.vertices = vertices;

        for (int j = 0; j < meshDimensions - 1; j++)
        {
            Wall.AddTriangle(j, j + meshDimensions + 1, j + meshDimensions);
            Wall.AddTriangle(j + meshDimensions + 1, j, j + 1);
        }

        return Wall;

    }

    public static MeshData GenerateMeshPlatform(MeshData terrainMesh, int meshDimensions, float height)
    {
        Vector3[] platformVertices = new Vector3[meshDimensions * meshDimensions];
        MeshData Platform = new MeshData(meshDimensions * meshDimensions, 2);

        for (int i = 0; i < meshDimensions * meshDimensions; i++)
        {
            platformVertices[i] = new Vector3(terrainMesh.vertices[i].x, height, terrainMesh.vertices[i].z);
        }

        Platform.vertices = platformVertices;
        Platform.uvs = terrainMesh.uvs;
        Platform.triangles = terrainMesh.triangles;

        return Platform;

    }
}
