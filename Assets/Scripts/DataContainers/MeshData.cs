using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

    int triangleIndex;

    public MeshData(int meshDim, int dim)
    {
        vertices = new Vector3[meshDim];
        uvs = new Vector2[meshDim];
        if (dim == 1)
        {
            triangles = new int[(meshDim - 1) * 6];
        }
        else
        {
            triangles = new int[((int)Math.Sqrt(meshDim) - 1) * ((int)Math.Sqrt(meshDim) - 1) * 6];
        }
    }

    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();

        return mesh;
    }

}
