using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateShapes
{
    public static MeshData GenerateCube(Vector3 center, float sideLength)
    {
        Vector3[] vertices = new Vector3[8];

        //Adding individual vertices
        vertices[0] = new Vector3(center.x - sideLength / 2, center.y - sideLength / 2, center.z - sideLength / 2);
        vertices[1] = new Vector3(center.x + sideLength / 2, center.y - sideLength / 2, center.z - sideLength / 2);
        vertices[2] = new Vector3(center.x + sideLength / 2, center.y + sideLength / 2, center.z - sideLength / 2);
        vertices[3] = new Vector3(center.x - sideLength / 2, center.y + sideLength / 2, center.z - sideLength / 2);
        vertices[4] = new Vector3(center.x - sideLength / 2, center.y + sideLength / 2, center.z + sideLength / 2);
        vertices[5] = new Vector3(center.x + sideLength / 2, center.y + sideLength / 2, center.z + sideLength / 2);
        vertices[6] = new Vector3(center.x + sideLength / 2, center.y - sideLength / 2, center.z + sideLength / 2);
        vertices[7] = new Vector3(center.x - sideLength / 2, center.y - sideLength / 2, center.z + sideLength / 2);

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

        return cube;
    }
}
